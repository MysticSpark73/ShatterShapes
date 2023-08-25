using ShatterShapes.Game.Input;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        
        private float _rotationX, _rotationY;
        private float _mouseSensitivity = 150;

        private void Awake()
        {
            InputEventsHandler.JoystickDirectionChanged += OnJoystickDirectionChanged;
        }

        private void OnApplicationQuit()
        {
            InputEventsHandler.JoystickDirectionChanged -= OnJoystickDirectionChanged;
        }

        private void OnJoystickDirectionChanged(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            _rotationX -= dir.x * _mouseSensitivity * Time.deltaTime;
            _rotationY -= dir.y * _mouseSensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            _rotationY = Mathf.Clamp(_rotationY, -45, 45);
            transform.localRotation = Quaternion.Euler( 0, -_rotationX, 0);
            _cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0 , 0);
        }
    }
}