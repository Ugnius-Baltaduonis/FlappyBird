using System.Timers;
class GameLogic
{
    //Constants
    private static readonly int MAPSIZE_X = 30;
    private static readonly int MAPSIZE_Y = 12;
    private static readonly int BIRD_START_X = 1;
    private static readonly int BIRD_START_Y = 1;
    private static readonly int GAPS_BETWEEEN_PILLARS = 7;
    private static readonly int PILLAR_HOLE_SIZE = 3;
    private static readonly int GRAVITY = 1;
    private static readonly int BIRD_FLAP_POWER = 3;
    private static readonly ConsoleKey FLAP_KEY = ConsoleKey.UpArrow;
    private static readonly ConsoleKey EXIT_KEY = ConsoleKey.Escape;
    private static readonly ConsoleKey START_KEY = ConsoleKey.Enter;

    private static int stepcounter = 0;
    private static int score = 0;
    private static GameState gameState = GameState.StartMenu;
    private static Bird bird = new Bird();
    private static List<Pillar> pillars = new List<Pillar>();
    private static Display display = new Display();
    private static System.Timers.Timer ticktimer = new System.Timers.Timer();
    public GameState GetGameState() => gameState;
    public void Run()
    {
        while (gameState != GameState.End)
        {
            while (gameState == GameState.StartMenu) this.RunStartMenu();
            while (gameState == GameState.Game) this.RunGame();
            while (gameState == GameState.GameOver) this.RunGameOverMenu();
        }
    }
    public void Start()
    {
        stepcounter = 0;
        score = 0;
        bird = new Bird(BIRD_START_X, BIRD_START_Y, BIRD_FLAP_POWER);
        pillars = new List<Pillar>();
        display = new Display(MAPSIZE_X, MAPSIZE_Y);
        ticktimer = new System.Timers.Timer();
        gameState = GameState.Game;
        display.FullClear();
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
            ConsoleKey key = Console.ReadKey(intercept: true).Key;
            if (key == FLAP_KEY) {
                bird.Flap();
            }
        }
    }
    public void RunStartMenu()
    {
        Console.Clear();
        Console.WriteLine("Press ENTER to start!");
        ConsoleKey key = Console.ReadKey(intercept: true).Key;
        if (key == START_KEY) this.Start();
    }

    public void RunGameOverMenu()
    {
        Console.Clear();
        display.DisplayScore(score);
        Console.WriteLine("Press ESCAPE to exit or ENTER to play again!");
        ConsoleKey key = Console.ReadKey(intercept: true).Key;
        if (key == EXIT_KEY) gameState = GameState.End;
        if (key == START_KEY) this.Start();
    }
    private void Tick(object? source, ElapsedEventArgs e)
    {
        display.Clear();
        if (stepcounter % GAPS_BETWEEEN_PILLARS == 0) pillars.Add(new Pillar(MAPSIZE_X - 1, MAPSIZE_Y, PILLAR_HOLE_SIZE));
        stepcounter++;
        for (int i = pillars.Count - 1; i >= 0; i--) {
            Pillar pillar = pillars[i];
            if (bird.GetX() == pillar.GetPos()) score++;
            pillar.SetPos(pillar.GetPos() - 1);
            if (!display.IsInBounds(pillar.GetPos(), 0)) pillars.RemoveAt(i);
            else display.AddObject(pillar);
        }
        bird.SetPos(bird.GetX(), bird.GetY() + GRAVITY);
        if (!display.IsInBounds(bird.GetX(), bird.GetY()) || display.IsObstacle(bird.GetX(), bird.GetY())) {
            this.Stop();
            return;
        }
        display.AddObject(bird);
        display.DrawMap();
        display.DisplayScore(score);
    }
    public List<Pillar> GetPillars() { return pillars; }
}