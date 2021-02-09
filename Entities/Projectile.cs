using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Projectile
{
    private Texture texture;
    private Vector2 position;
    private float direction;
    private float speed;

    public Projectile(Texture texture, Vector2 position, float direction, float speed)
    {
        this.texture = texture;
        this.position = position;
        this.direction = direction;
        this.speed = speed;
    }

    public void Render()
    {
        position.X += speed * direction;
        Engine.DrawTexture(texture, position);
    }

    public void Render(Vector2 offset)
    {
        position.X += speed * direction;
        Engine.DrawTexture(texture, position - offset);
    }
}
