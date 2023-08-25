using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.ShatteredObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShatterShapes.Game.Level
{
    public class LevelShapesCreator : MonoBehaviour
    {
        [SerializeField] private levelStgeController _levelStgeController;
        [SerializeField] private Transform _objectsContainer;
        [SerializeField] private ObjectPooler _objectPooler;

        private readonly Vector2Int distBounds = new Vector2Int(5, 10);
        private readonly Vector2Int heightBounds = new Vector2Int(3, 6);


        private void Awake()
        {
            LevelEventsHandler.StageComplete += OnStageComplete;
        }

        public void CreateQuad(int a)
        {
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    Vector3 offset = new Vector3(j, i, 0);
                    PoolObject(transform.position + offset);
                }
            }
        }

        public void CreateRect(int a, int b)
        {
            for (int i = 0; i < a; i++) 
            {
                for (int j = 0; j < b; j++)
                {
                    Vector3 offset = new Vector3(j, i, 0);
                    PoolObject(transform.position + offset);
                }
            }
        }

        public void CreatePyramid(int h)
        {
            int blocks = 1;
            for (int i = h; i >= 0; i--)
            {
                for (int j = 0; j < blocks; j++)
                {
                    Vector3 offset = new Vector3(j - Mathf.Floor(blocks/2.0f), i, 0);
                    PoolObject(transform.position + offset);
                }
                blocks += 2;
            }
        }

        public void CreateCircle(int r)
        {
            for (int i = 0; i < r; i++)
            {
                int objectsInCircle = Mathf.Max(1, i * 8);
                for (int j = 1; j <= objectsInCircle; j++)
                {
                    Vector3 offset = new Vector3( 
                        Mathf.RoundToInt(Mathf.Sin(Mathf.Deg2Rad * j * (float)(360.0f/objectsInCircle)) * i), 
                        Mathf.RoundToInt(Mathf.Cos(Mathf.Deg2Rad * j * (float)(360.0f/objectsInCircle)) * i),
                        0);

                    PoolObject(transform.position + offset);
                }
            }
        }

        public void CreateRandomShape()
        {
            int indx = Random.Range(0, 4);

            switch (indx)
            {
                case 0:
                    CreateQuad(5);
                    break;
                case 1:
                    CreateRect(5, 7);
                    break;
                case 2: 
                    CreatePyramid(5);
                    break;
                case 3:
                    CreateCircle(3);
                    break;
            }
        }

        private void PoolObject(Vector3 position)
        { 
            var obj = _objectPooler.SpawnFromPool(_objectPooler.GetRandomPool(), position, _objectsContainer);
            var shatteredObject = obj as ShatteredObject;
            if (shatteredObject != null)
            {
                _levelStgeController.AddToShape(shatteredObject);
            }
        }

        private void OnStageComplete()
        {
            _levelStgeController.EraseShape();
            SetRandomLocation();
            CreateRandomShape();
        }

        private void SetRandomLocation()
        {
            transform.position = new Vector3(
                Random.Range((float) distBounds.x, (float) distBounds.y),
                Random.Range((float) heightBounds.x, (float) heightBounds.y),
                Random.Range((float) distBounds.x, (float) distBounds.y));
        }
        
    }
}
