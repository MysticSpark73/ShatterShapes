using System;
using System.Collections.Generic;
using System.Linq;
using ShatterShapes.Extensions;
using ShatterShapes.Game.Level;
using ShatterShapes.ShatteredObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShatterShapes.Core.Object_Pooling
{
    public class ObjectPooler : MonoBehaviour, IInitable
    {
        [SerializeField] private List<PoolableObject> _objectPools;
        [SerializeField] private LevelController _levelController;

        private Dictionary<ObjectsPool, Queue<IPoolable>> _poolsDictionary;

        public void Init(object[] args = null)
        {
            LevelEventsHandler.ObjectExpired += OnObjectExpired;
            GeneratePools();
        }

        private void OnApplicationQuit()
        {
            LevelEventsHandler.ObjectExpired -= OnObjectExpired;
        }

        public IPoolable SpawnFromPool(ObjectsPool key, Vector3 position, Transform container = null)
        {
            if (!_poolsDictionary.ContainsKey(key))
            {
                Debug.LogError($"The key {key} is not present in the pools dictionary");
                return null;
            }

            var pooledObject = _poolsDictionary[key].Dequeue();
            
            if (pooledObject == null)
            {
                Debug.LogError($"The pool with key {key} is empty");
                return null;
            }
            pooledObject.SetPosition(position, container);
            pooledObject.OnPooled();
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnToPool(ObjectsPool key, IPoolable obj)
        {
            obj.SetActive(false);
            obj.OnReturn();
            if (!_poolsDictionary.ContainsKey(key))
            {
                Debug.LogError($"There is no key {key} in pools dictionary");
                return;
            }
            obj.SetPosition( Vector3.zero, _objectPools.FirstOrDefault(p => p.tag == key).poolContainer);
            _poolsDictionary[key].Enqueue(obj);
        }

        public ObjectsPool GetRandomPool() => _objectPools[Random.Range(0, _objectPools.Count)].tag;

        private void GeneratePools()
        {
            _poolsDictionary = new Dictionary<ObjectsPool, Queue<IPoolable>>();
            foreach (var pool in _objectPools)
            {
                _poolsDictionary.AddSafe(pool.tag, new Queue<IPoolable>());
                for (int i = 0; i < pool.poolSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, pool.poolContainer);
                    obj.name = $"{pool.tag} {i}";
                    IPoolable poolableObject = obj.GetComponent<IPoolable>();
                    if (poolableObject == null)
                    {
                        Debug.LogError($"Poolable object's prefab with the tag {pool.tag} don't have IPoolable component");
                        break;
                    }
                    poolableObject.KeyPool = pool.tag;
                    _poolsDictionary[pool.tag].Enqueue(poolableObject);
                    obj.SetActive(false);
                }
            }
        }

        private void OnObjectExpired(ObjectsPool pool, IPoolable obj)
        {
            ReturnToPool(pool, obj);
        }

        [System.Serializable]
        private struct PoolableObject
        {
            public ObjectsPool tag;
            public GameObject prefab;
            public Transform poolContainer;
            public int poolSize;
        }
    }
}
