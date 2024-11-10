using System.Timers;
class GameLogic
{
    public static readonly int mapsizeX = 30;
    public static readonly int mapsizeY = 12;
    private static readonly int birdStartX = 1;
    private static readonly int birdStartY = 1;
    private static readonly int gapsBetweenPillars = 7;
    private static int stepcounter = 0;
    private static int score = 0;
    private static GameState gameState = GameState.StartMenu;
    private static Bird bird = new Bird(birdStartX, birdStartY);
    private static List<Pillar> pillars = new List<Pillar>();
    private static Display display = new Display(mapsizeX, mapsizeY);
    private static System.Timers.Timer ticktimer = new System.Timers.Timer();
    public GameState GetGameState() => gameState;
    public void Start()
    {
        gameState = GameState.Game;
        ticktimer.Elapsed += new ElapsedEventHandler(Tick);
        ticktimer.Interval = 200;
        ticktimer.Enabled = true;
    }
    public void Stop()
    {
        gameState = GameState.GameOver;
        bird.Kill();
        ticktimer.Stop();
        ticktimer.Dispose();
    }

    public void RunGame()
    {
        if (Console.KeyAvailable) {
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.UpArrow) {
                bird.Flap();
            }
        }
    }
    public void RunStartMenu()
    {
        Console.Clear();
        Console.WriteLine("Press enter to start!");
        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.Enter) this.Start();
    }

    public void RunGameOverMenu()
    {
        Console.Clear();
        display.DisplayScore(score);
        Console.WriteLine("Press escape to exit!");
        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.Escape) gameState = GameState.End;
    }
    private void Tick(object? source, ElapsedEventArgs e)
    {
        display.Clear();
        if (stepcounter % gapsBetweenPillars == 0) pillars.Add(new Pillar(mapsizeX - 1, mapsizeY));
        stepcounter++;
        for (int i = pillars.Count - 1; i >= 0; i--) {
            Pillar pillar = pillars[i];
            if (bird.GetX() == pillar.GetPos()) score++;
            pillar.SetPos(pillar.GetPos() - 1);
            if (!display.IsInBounds(pillar.GetPos(), 0)) pillars.RemoveAt(i);
            else display.AddPillar(pillar);
        }
        bird.SetPos(bird.GetX(), bird.GetY() + 1);
        if (!display.IsInBounds(bird.GetX(), bird.GetY()) || display.IsObstacle(bird.GetX(), bird.GetY())) {
            this.Stop();
            return;
        }
        display.AddBird(bird);
        display.DrawMap();
        display.DisplayScore(score);
    }
}