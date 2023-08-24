using UnityEngine;

namespace ShatterShapes
{
    public class ShapeCreator : MonoBehaviour
    {
        [SerializeField] private Transform _objectsContainer;
        
        private void Start()
        {
            CreateCircle(10);
        }

        public void CreateQuad(int a)
        {
            for (int i = 0; i < a; i++) 
            {
               for (int j = 0; j < a; j++)
               {
                   Vector3 offset = new Vector3(j, i, 0);
                   var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                   obj.transform.SetParent(_objectsContainer);
                   obj.transform.position = transform.position + offset;
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
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.SetParent(_objectsContainer);
                    obj.transform.position = transform.position + offset;
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
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.SetParent(_objectsContainer);
                    obj.transform.position = transform.position + offset;
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
                    var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.SetParent(_objectsContainer);
                    obj.transform.position = transform.position + offset;
                }
            }
        }
        
    }
}
