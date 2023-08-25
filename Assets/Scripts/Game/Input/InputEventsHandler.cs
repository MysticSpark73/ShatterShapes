using System;
using UnityEngine;

namespace ShatterShapes.Game.Input
{
    public static class InputEventsHandler
    {
        public static Action<Vector2> JoystickDirectionChanged;
        public static Action<Vector3> PlayerFirePressed;
    }
}
