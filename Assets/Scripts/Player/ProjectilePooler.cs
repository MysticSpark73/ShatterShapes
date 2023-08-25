using System;
using System.Collections.Generic;
using ShatterShapes.Core;
using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.Game.Level;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class ProjectilePooler : MonoBehaviour, IInitable
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _projectilesContainer;
        [SerializeField] private int _poolSize;

        private Queue<IPoolable> _projectilesPool;
        
        public void Init(object[] args = null)
        {
            LevelEventsHandler.ProjectileExpired += OnProjectileExpired;
            GeneratePool();
        }

        private void OnApplicationQuit()
        {
            LevelEventsHandler.ProjectileExpired -= OnProjectileExpired;
        }

        public IPoolable SpawnFromPool(Vector3 pos, Transform parent = null)
        {
            if (_projectilesPool.Count == 0) return null;
            var projectile = _projectilesPool.Dequeue();
            projectile.SetPosition(pos, parent);
            projectile.OnPooled();
            projectile.SetActive(true);
            return projectile;
        }

        public void ReturnToPool(IPoolable obj)
        {
            obj.SetActive(false);
            obj.OnReturn();
            obj.SetPosition(Vector3.zero, _projectilesContainer);
            _projectilesPool.Enqueue(obj);
        }

        private void GeneratePool()
        {
            _projectilesPool = new Queue<IPoolable>();
            for (int i = 0; i < _poolSize; i++)
            {
                var obj = Instantiate(_projectilePrefab, _projectilesContainer);
                var pooledObject = obj.GetComponent<IPoolable>();
                if (pooledObject == null)
                {
                    Debug.LogError($"pooled projectile does not contain IPoolable component");
                    break;
                }
                obj.SetActive(false);
                _projectilesPool.Enqueue(pooledObject);
            }
        }

        private void OnProjectileExpired(IPoolable obj)
        {
            ReturnToPool(obj);
        }
    }
}