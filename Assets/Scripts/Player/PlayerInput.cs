using ShatterShapes.Game.Input;
using ShatterShapes.UI;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private OnPointerDownListener _touchDetector;

        private float _mouseSensitivity = 100;
        private float _rotationX, _rotationY;

        private void Awake()
        {
            InputEventsHandler.JoystickDirectionChanged += OnJoystickDirectionChanged;
            _touchDetector.AddListener(OnTouch);
        }

        private void OnApplicationQuit()
        {
            InputEventsHandler.JoystickDirectionChanged -= OnJoystickDirectionChanged;
        }

        private void OnJoystickDirectionChanged(Vector2 dir)
        {
            Debug.Log(dir);
            if (dir == Vector2.zero) return;
            _rotationX -= dir.x * _mouseSensitivity * Time.deltaTime;
            _rotationY -= dir.y * _mouseSensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            _rotationY = Mathf.Clamp(_rotationY, -45, 45);
            transform.localRotation = Quaternion.Euler( 0, -_rotationX, 0);
            _cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0 , 0);
        }

        private void OnTouch()
        {
            /*if (Input.GetMouseButton(0))
            {*/
                Debug.DrawRay(transform.position, _playerCamera.ScreenPointToRay(Input.mousePosition).direction,
                    Color.red);
                InputEventsHandler.PlayerFirePressed?.Invoke(_playerCamera.ScreenPointToRay(Input.mousePosition)
                    .direction);
            /*}*/
        }
    }
}
