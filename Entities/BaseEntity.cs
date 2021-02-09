using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class BaseEntity
{
    protected Bounds2 bounds;
    protected Texture texture;

    protected Vector2 velocity;

    public BaseEntity(Bounds2 bounds, Texture texture)
    {
        this.bounds = bounds;
        this.texture = texture;
    }

    public abstract void Input();

    public abstract void Update();

    public abstract void Render(Vector2 offset);

    public Bounds2 GetBounds()
    {
        return bounds;
    }

    public void SetBounds(Bounds2 bounds)
    {
        this.bounds = bounds;
    }

    public Vector2 GetPosition()
    {
        return bounds.Position;
    }

    public void SetPosition(Vector2 position)
    {
        bounds.Position = position;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public bool WillHitEntity(BaseEntity other)
    {
        Bounds2 boundsA = new Bounds2(bounds.Position + velocity, bounds.Size);
        Bounds2 boundsB = new Bounds2(other.GetPosition() + other.GetVelocity(), other.GetBounds().Size);

        return boundsA.Overlaps(boundsB);
    }

    public bool WillHit(Bounds2 other)
    {
        Bounds2 boundsA = new Bounds2(bounds.Position + velocity, bounds.Size);
        Bounds2 boundsB = new Bounds2(other.Position, other.Size);
        return boundsA.Overlaps(boundsB);
    }

    public bool Hits(Bounds2 other)
    {
        return bounds.Overlaps(other);
    }
}
