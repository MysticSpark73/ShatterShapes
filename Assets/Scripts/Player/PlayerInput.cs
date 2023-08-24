using ShatterShapes.Game.Input;
using UnityEngine;

namespace ShatterShapes.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

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
            //Rotate Camera
        }
    }
}
