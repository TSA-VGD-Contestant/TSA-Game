using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RayGun : Weapon
{
    //false = right, true = left
    private bool flipped;
    public RayGun(Texture texture, float damage) : base(texture, damage)
    {
        flipped = false;
    }

    public override void Render(Vector2 pos, TextureMirror mirror)
    {
        flipped = (mirror == TextureMirror.Horizontal);
        Engine.DrawTexture(texture, pos, mirror:mirror);
    }

    public bool IsFlipped()
    {
        return flipped;
    }
}
