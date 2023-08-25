namespace ShatterShapes.Data
{
    [System.Serializable]
    public class LevelColorsConfig
    {
        public LevelColor[] _levelColors;
    }

    [System.Serializable]
    public class LevelColor
    {
        public string name;
        public string hex;
    }
}