using System;
using ShatterShapes.Core.Object_Pooling;
using UnityEngine;

namespace ShatterShapes.Game.Level
{
    public static class LevelEventsHandler
    {
        public static Action<ObjectsPool, IPoolable> ObjectExpired;
        public static Action<IPoolable> ProjectileExpired;
        public static Action StageComplete;
        public static Action ShapeObjectDamaged;
        public static Action<Vector3> NewStageGenerated;
    }
}
