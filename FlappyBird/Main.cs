internal class FlappyBird
{
    private static void Main(string[] args)
    {
        GameLogic gameLogic = new GameLogic();
        while (gameLogic.GetGameState() == GameState.StartMenu) gameLogic.RunStartMenu();
        while (gameLogic.GetGameState() == GameState.Game) gameLogic.RunGame();
        while (gameLogic.GetGameState() == GameState.GameOver) gameLogic.RunGameOverMenu();
    }
}