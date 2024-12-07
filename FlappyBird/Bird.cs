class Bird : Entity, IFlappable
{
    private char sprite = '>';
    private int flapPower;
    public Bird(int posx, int posy)
    {
        this.posx = posx;
        this.posy = posy;
        this.alive = true;
        this.flapPower = 3;
    }
    public Bird() { }
    public Bird(int posx, int posy, int fp)
    {
        if(posx < 0) posx = 0;
        if(posy < 0) posy = 0;
        this.posx = posx;
        this.posy = posy;
        this.alive = true;
        this.flapPower = fp;
    }

    public char GetSprite() => sprite;
    public void Flap()
    {
        posy -= flapPower;
        if (posy < 0) posy = 0;
    }
}
