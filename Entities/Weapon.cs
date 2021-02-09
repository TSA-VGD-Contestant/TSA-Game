using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class Weapon
{
    protected Texture texture;
    protected readonly float damage;

    public Weapon(Texture texture, float damage)
    {
        this.texture = texture;
        this.damage = damage;
    }
    public abstract void Render(Vector2 pos, TextureMirror mirror);
}

