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

        private float _shootForce = 700;
        
        public ObjectsPool KeyPool { get; set; }

        public void OnPooled()
        {
            _renderer.materials[0].color = Parameters.GetRandomLevelColor();
        }

        public void OnReturn()
        {
            _rigidbody.isKinematic = true;
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
            await new WaitForSeconds(10);
            LevelEventsHandler.ProjectileExpired?.Invoke(this);
        }

    }
}