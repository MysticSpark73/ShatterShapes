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
            _touchDetector.AddListener(OnTouch);
        }

        private void OnTouch()
        {
            Debug.DrawRay(transform.position, _playerCamera.ScreenPointToRay(Input.mousePosition).direction,
                Color.red);
            InputEventsHandler.PlayerFirePressed?.Invoke(_playerCamera.ScreenPointToRay(Input.mousePosition)
                .direction);
        }
    }
}
