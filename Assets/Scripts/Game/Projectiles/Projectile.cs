using DG.Tweening;
using ShatterShapes.Core;
using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.Game.Level;
using UnityEngine;

namespace ShatterShapes.Game.Projectiles
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _renderer;

        private Vector3 _initialScale;
        private float _shootForce = 700;
        private float _disappearTime = 5;
        
        public ObjectsPool KeyPool { get; set; }

        public void OnPooled()
        {
            _initialScale = transform.localScale;
            _renderer.materials[0].color = Parameters.GetRandomLevelColor();
        }

        public void OnReturn()
        {
            _rigidbody.isKinematic = true;
            transform.localScale = _initialScale;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
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

        public void Shoot(Vector3 dir)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(dir * _shootForce);
            DestroyAsync();
        }

        private async void DestroyAsync()
        {
            await new WaitForSeconds(_disappearTime / 10 * 8);
            bool isComplete = false;
            transform.DOScale(Vector3.zero, _disappearTime / 10 * 2)
                .OnComplete(() => isComplete = true);
            await new WaitUntil(() => isComplete);
            LevelEventsHandler.ProjectileExpired?.Invoke(this);
        }

    }
}