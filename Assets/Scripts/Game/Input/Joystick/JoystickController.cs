using System;
using UnityEngine;

namespace ShatterShapes.Game.Input.Joystick
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private global::Joystick _joystick;

        public Vector2 Direction => _joystick.Direction;


        private void Update()
        {
            InputEventsHandler.JoystickDirectionChanged?.Invoke(Direction);
        }
    }
}
