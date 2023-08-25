using System.Collections.Generic;
using ShatterShapes.Core.Object_Pooling;
using UnityEngine;

namespace ShatterShapes.ShatteredObjects
{
    public class ShatteredObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private List<ShatteredPiece> _pieces;
        
        public void OnPooled()
        {
            foreach (var piece in _pieces)
            {
                piece.Init();
            }
        }

        public void OnReturn()
        {
            foreach (var piece in _pieces)
            {
                piece.ResetPiece();
            }
        }

        public void SetPosition(Vector3 pos, Transform container = null)
        {
            transform.position = pos;
            if (container != null)
            {
                transform.SetParent(container);
            }
        }

        public void SetActive(bool value) => gameObject.SetActive(value);
        public void SetParent(Transform parent) => transform.SetParent(parent);

        private void OnCollisionEnter(Collision collision)
        {
            foreach (var piece in _pieces)
            {
                piece.OnShatter();
            }
        }
    }
}
