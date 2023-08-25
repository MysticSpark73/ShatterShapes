namespace ShatterShapes.Game
{
    public static class GameStateController
    {
        public static GameState CurrentGameState { get; private set; }

        public static void SetGameState(GameState state) => CurrentGameState = state;
    }
}