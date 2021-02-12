using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Cloud
{
    private Bounds2 bounds;
    private float speed;
    private readonly Texture texture = Engine.LoadTexture("Cloud.png");
    public Cloud(Bounds2 bounds, float speed)
    {
        this.bounds = bounds;
        this.speed = speed;
    }
    public void Update(float offset)
    {        
        Engine.DrawTexture(texture, new Vector2(bounds.Position.X - offset, bounds.Position.Y), size: bounds.Size);
    }
    public Bounds2 GetBounds()
    {
        return bounds;
    }
    public void SetBounds(Bounds2 bounds)
    {
        this.bounds = bounds;
    }
}