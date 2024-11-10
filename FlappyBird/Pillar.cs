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
        holesize = 5;
    }

    public int GetPos() => pos;
    public int GetHolepos() => holepos;
    public int GetHolesize() => holesize;
    public void SetPos(int pos) => this.pos = pos;
}
