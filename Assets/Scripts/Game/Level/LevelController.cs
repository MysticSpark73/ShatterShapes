using System.Collections.Generic;
using ShatterShapes.Core;
using ShatterShapes.Core.Object_Pooling;
using UnityEngine;
using Color = UnityEngine.Color;

namespace ShatterShapes.Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private LevelShapesCreator _levelShapesCreator;

        private LevelColorsConfigLoader _colorsConfigLoader;

        private void Awake()
        {
            _colorsConfigLoader = new LevelColorsConfigLoader();
            LoadLevelColors();
            _objectPooler.Init();
            _levelShapesCreator.CreateRandomShape();
            GameStateController.SetGameState(GameState.Playing);
        }

        private void LoadLevelColors()
        {
            var colorsConfig = _colorsConfigLoader.LoadLevelColors();
            List<Color> levelColors = new List<Color>();
            foreach (var color in colorsConfig)
            {
                ColorUtility.TryParseHtmlString(color.hex, out var parsedColor);
                levelColors.Add(parsedColor);
            }
            Parameters.SetLevelColors(levelColors);
        }

        
    }
}
