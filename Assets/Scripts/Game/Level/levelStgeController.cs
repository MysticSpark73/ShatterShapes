using System.Collections.Generic;
using System.Linq;
using ShatterShapes.ShatteredObjects;
using UnityEngine;

namespace ShatterShapes.Game.Level
{
    public class levelStgeController : MonoBehaviour
    {
        private List<ShatteredObject> _currentShape = new List<ShatteredObject>();

        public bool IsStageComplete => _currentShape.Count > 0 && _currentShape.All(o => o.IsShattered);

        private void Awake()
        {
            LevelEventsHandler.ShapeObjectDamaged += OnShapeDamaged;
        }

        private void OnApplicationQuit()
        { 
            LevelEventsHandler.ShapeObjectDamaged += OnShapeDamaged;
        }

        public void AddToShape(ShatteredObject obj) => _currentShape.Add(obj);

        public void EraseShape() => _currentShape.Clear();

        private void OnShapeDamaged()
        {
            if (IsStageComplete)
            {
                LevelEventsHandler.StageComplete?.Invoke();
            }
        }
    }
}