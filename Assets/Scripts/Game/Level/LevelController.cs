using System.Collections.Generic;
using System.Drawing;
using ShatterShapes.Core.Object_Pooling;
using ShatterShapes.Extensions;
using UnityEngine;
using Color = UnityEngine.Color;

namespace ShatterShapes.Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private LevelShapesCreator _levelShapesCreator;

        private LevelColorsConfigLoader _colorsConfigLoader;
        private List<Color> _levelColors;

        private void Awake()
        {
            _colorsConfigLoader = new LevelColorsConfigLoader();
            LoadLevelColors();
            _objectPooler.Init();
            _levelShapesCreator.CreateRandomShape();
            LevelEventsHandler.LevelReady?.Invoke();
        }

        public Color GetRandomLevelColor()
        {
            if (_levelColors.Count == 0) return Color.clear;
            return _levelColors[Random.Range(0, _levelColors.Count)];
        }

        private void LoadLevelColors()
        {
            var colorsConfig = _colorsConfigLoader.LoadLevelColors();
            _levelColors = new List<Color>();
            foreach (var color in colorsConfig)
            {
                ColorUtility.TryParseHtmlString(color.hex, out var parsedColor);
                _levelColors.Add(parsedColor);
            }
        }

        
    }
}
