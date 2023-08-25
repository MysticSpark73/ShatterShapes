using ShatterShapes.Game.Input;
using ShatterShapes.Game.Projectiles;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private Transform _gunTransform;
        [SerializeField] private ProjectilePooler _projectilePooler;

        private Projectile _currentProjectile;
        private float _fireRate = .33f;
        private float _lastFiredTime;

        private void Awake()
        {
            InputEventsHandler.PlayerFirePressed += OnPlayerFirePressed;
            _lastFiredTime = Time.time;
            _projectilePooler.Init();
            _currentProjectile = _projectilePooler.SpawnFromPool(_gunTransform.position) as Projectile;
        }

        private void OnApplicationQuit()
        {
            InputEventsHandler.PlayerFirePressed -= OnPlayerFirePressed;
        }

        private void OnPlayerFirePressed(Vector3 direction)
        {
            if (Time.time - _lastFiredTime > _fireRate)
            {
                _currentProjectile.Shoot(direction);
                var pooledObject = _projectilePooler.SpawnFromPool(_gunTransform.position);
                var projectile = pooledObject as Projectile;
                if (projectile == null) return;
                _currentProjectile = projectile;
                _lastFiredTime = Time.time;
            }
        }
    }
}