using System.Collections.Generic;
using UnityEngine;

namespace ShatterShapes.Core
{
    public static class Parameters
    {
        public static readonly string _levelColorsJSONPath = "Data/Colors";

        public static List<Color> _levelColors;

        public static void SetLevelColors(List<Color> colors) => _levelColors = colors;

        public static Color GetRandomLevelColor()
        {
            return _levelColors != null ? _levelColors[Random.Range(0, _levelColors.Count)] : Color.clear;
        }
    }
}