internal class FlappyBird
{
    private static void Main(string[] args)
    {
        Tests.RunTests();
        GameLogic gameLogic = new GameLogic();
        gameLogic.Run();
    }
}
