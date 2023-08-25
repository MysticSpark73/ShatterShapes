using System.Collections.Generic;
using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.Game.Level;
using UnityEngine;

namespace ShatterShapes.ShatteredObjects
{
    public class ShatteredObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private List<ShatteredPiece> _pieces;

        private LevelController _levelController;

        public ObjectsPool KeyPool { get; set; }

        public void OnPooled()
        {
            Color color = _levelController != null ? _levelController.GetRandomLevelColor() : Color.clear;
            foreach (var piece in _pieces)
            {
                piece.Init();
                if (color != Color.clear)
                {
                    piece.SetColor(color);
                }
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

        public void SetLevelController(LevelController levelController) => _levelController = levelController;

        private void OnCollisionEnter(Collision collision)
        {
            foreach (var piece in _pieces)
            {
                piece.OnShatter();
            }
        }
    }
}
