class Display
{
    private readonly char[,] emptyMap = new char[,] {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', },
        };
    private readonly int mapsizeY;
    private readonly int mapsizeX;
    private char[,] currentMap;

    public Display(int mapsizeX, int mapsizeY)
    {
        currentMap = (char[,])emptyMap.Clone();
        this.mapsizeX  = mapsizeX;
        this.mapsizeY = mapsizeY;
    }

    public void DrawMap()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < mapsizeY; i++) {
            for (int j = 0; j < mapsizeX; j++) Console.Write(currentMap[i, j]);
            Console.Write('\n');
        }
    }
    public void Clear() => currentMap = (char[,])emptyMap.Clone();
    public void AddBird(Bird bird) 
    {
        if (!IsInBounds(bird.GetX(), bird.GetY())) bird.SetPos(bird.GetX(), 0);
        currentMap[bird.GetY(), bird.GetX()] = bird.GetSprite();
    }
    public void AddPillar(Pillar pillar)
    {
        if (!IsInBounds(pillar.GetPos(),0)) return;
        for(int i = 1;  i < mapsizeY; i++) {
            if (i < pillar.GetHolepos() - 1 || i > pillar.GetHolepos() + 1) currentMap[i, pillar.GetPos()] = '#';
        }
    }
    public bool IsObstacle(int x, int y)
    {
        if (currentMap[y, x] == '#') return true;
        else return false;
    }
    public bool IsInBounds(int x, int y)
    {
        if (x < 0 || y < 0) return false;
        if (x >= mapsizeX) return false;
        if (y >= mapsizeY) return false;
        return true;
    }
    public void DisplayScore(int score) => Console.WriteLine("Score: " + score);
}