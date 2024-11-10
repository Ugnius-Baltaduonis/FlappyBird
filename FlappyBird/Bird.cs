class Bird : Entity, IFlappable
{
    private char sprite = '>';
    public Bird(int posx, int posy)
    {
        this.posx = posx;
        this.posy = posy;
        this.alive = true;
    }

    public char GetSprite() => sprite;
    public void Flap()
    {
        posy -= 3;
        if (posy < 0) posy = 0;
    }
}
