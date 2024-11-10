class Entity
{
    protected int posx;
    protected int posy;
    protected bool alive;

    public Entity() { }
    public Entity(int posx, int posy)
    {
        this.posx = posx;
        this.posy = posy;
        alive = true;
    }

    public void SetPos(int posx, int posy)
    {
        this.posx = posx;
        this.posy = posy;
    }
    public bool IsAlive() => alive;
    public void Kill() => alive = false;
    public int GetX() { return posx; }
    public int GetY() { return posy; }
}
