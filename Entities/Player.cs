using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Player
{
    //Player Texture
    //TODO: Player Animation
    private readonly Texture texture = Engine.LoadTexture("PlayerForward.png");

    private readonly float ScreenX;
    private float ScreenY;
    private float Offset;
    private readonly float Width = 64;
    private readonly float Height = 64;

    //Movement variables
    private const float ACCELX = 1f;
    private const float MAXVELX = 5f;
    private float VelocityX;
    private float VelocityY;

    //Movement states
    //Walk state
    public enum WSTATE
    {
        STANDING,
        LEFT,
        RIGHT
    };

    private WSTATE WalkState;

    public Player()
    {
        ScreenX = Game.Resolution.X / 2 - Width / 2;
        ScreenY = Game.Resolution.Y / 2 - Height / 2;
        Offset = -32;

        WalkState = WSTATE.STANDING;
    }

    public void Input()
    {
        if(Engine.GetKeyHeld(Key.A))
        {
            WalkState = WSTATE.LEFT;
        }
        else if(Engine.GetKeyHeld(Key.D))
        {
            WalkState = WSTATE.RIGHT;
        }
        else
        {
            WalkState = WSTATE.STANDING;
        }
    }

    public void Update()
    {
        Movement();
    }

    public void Render()
    {
        Engine.DrawTexture(texture, new Vector2(ScreenX, ScreenY));
    }

    private void Movement()
    {
        switch (WalkState)
        {
            case WSTATE.LEFT:
                if (VelocityX - ACCELX >= -MAXVELX)
                {
                    VelocityX -= ACCELX;
                }
                else
                {
                    VelocityX = -MAXVELX;
                }
                break;
            case WSTATE.RIGHT:
                if (VelocityX + ACCELX <= MAXVELX)
                {
                    VelocityX += ACCELX;
                }
                else
                {
                    VelocityX = MAXVELX;
                }
                break;
            case WSTATE.STANDING:
                VelocityX = 0;
                break;
        }

        Offset += VelocityX;
    }

    public bool HitsBounds(Bounds2 other)
    {
        return new Bounds2(ScreenX, ScreenY, Width, Height).Overlaps(other);
    }

    public Bounds2 GetBounds()
    {
        return new Bounds2(ScreenX, ScreenY, Width, Height);
    }

    public float GetOffset()
    {
        return Offset;
    }
}
