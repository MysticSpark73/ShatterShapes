using ShatterShapes.Game;
using ShatterShapes.Game.Input;
using ShatterShapes.UI.Camera;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class PlayerLookController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        private CameraTransitionController _transitionController;
        private float _rotationX, _rotationY;
        private float _mouseSensitivity = 150;

        private void Awake()
        {
            InputEventsHandler.JoystickDirectionChanged += OnJoystickDirectionChanged;
            _transitionController = new CameraTransitionController(_cameraTransform);
        }

        private void OnApplicationQuit()
        {
            _transitionController.Dispose();
            InputEventsHandler.JoystickDirectionChanged -= OnJoystickDirectionChanged;
        }

        public Transform GetTransform => transform;

        private void OnJoystickDirectionChanged(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            if (GameStateController.CurrentGameState != GameState.Playing) return;

            _rotationX -= dir.x * _mouseSensitivity * Time.deltaTime;
            _rotationY -= dir.y * _mouseSensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            _rotationY = Mathf.Clamp(_rotationY, -45, 45);
            transform.localRotation = Quaternion.Euler( 0, -_rotationX, 0);
            _cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0 , 0);
        }
    }
}