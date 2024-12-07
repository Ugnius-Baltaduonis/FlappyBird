class Pillar
{
    private int pos;
    private int holepos;
    private int holesize;
    
    public Pillar(int pos, int mapsizeY)
    {
        Random random = new Random();
        this.pos = pos;
        holepos = random.Next(3, mapsizeY-3);
        holesize = 3;
    }
    public Pillar(int pos, int mapsizeY, int holesize)
    {
        Random random = new Random();
        if (pos < 0) pos = 0;
        this.pos = pos;
        holepos = random.Next(3, mapsizeY - 3);
        this.holesize = holesize;
    }

    public int GetPos() => pos;
    public int GetHolepos() => holepos;
    public int GetHolesize() => holesize;
    public void SetPos(int pos) => this.pos = pos;
}
