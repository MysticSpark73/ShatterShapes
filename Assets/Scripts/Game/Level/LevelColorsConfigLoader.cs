using System.IO;
using Leguar.TotalJSON;
using ShatterShapes.Core;
using ShatterShapes.Data;
using UnityEngine;

namespace ShatterShapes.Game.Level
{
    public class LevelColorsConfigLoader
    {
        public LevelColor[] LoadLevelColors()
        {
            var asset = Resources.Load<TextAsset>(Parameters._levelColorsJSONPath);
            string json = asset.text;
            var jarray = JArray.ParseString(json);
            LevelColor[] config = new LevelColor[jarray.Length];
            for (int i = 0; i < jarray.Length; i++)
            {
                config[i] = jarray.GetJSON(i).Deserialize<LevelColor>();
            }

            return config;
        }
    }
}