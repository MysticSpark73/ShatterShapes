using System.Collections.Generic;
using ShatterShapes.Core;
using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.Game.Level;
using UnityEngine;

namespace ShatterShapes.ShatteredObjects
{
    public class ShatteredObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private List<ShatteredPiece> _pieces;
        [SerializeField] private Collider _selfCollider;

        private bool _isShattered;
        private float _disappearDuration = 12;

        private readonly string _projectileTag = "Projectile";

        public ObjectsPool KeyPool { get; set; }
        public bool IsShattered => _isShattered;

        public void OnPooled()
        {
            _selfCollider.enabled = true;
            _isShattered = false;
            Color color = Parameters.GetRandomLevelColor();
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

        private void OnCollisionEnter(Collision collision)
        {
            if (_isShattered) return;
            if (collision.gameObject.CompareTag(_projectileTag))
            {
                _selfCollider.enabled = false;
                foreach (var piece in _pieces)
                {
                    piece.OnShatter();
                }
                _isShattered = true;
                LevelEventsHandler.ShapeObjectDamaged?.Invoke();
                DestroyAsync();
            }
        }

        private async void DestroyAsync()
        {
            await new WaitForSeconds(_disappearDuration);
            LevelEventsHandler.ObjectExpired?.Invoke(KeyPool, this);
        }
    }
}
