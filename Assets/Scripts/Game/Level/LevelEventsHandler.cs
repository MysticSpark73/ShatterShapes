using System;
using ShatterShapes.Core.Object_Pooling;

namespace ShatterShapes.Game.Level
{
    public static class LevelEventsHandler
    {
        public static Action LevelReady;
        public static Action<ObjectsPool, IPoolable> ObjectExpired;
        public static Action<IPoolable> ProjectileExpired;
        public static Action StageComplete;
        public static Action ShapeObjectDamaged;
    }
}
