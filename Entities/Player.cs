using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Player : BaseEntity
{
    public enum WALKINGSTATES
    {
        LEFT,
        RIGHT,
        STANDING
    }

    private WALKINGSTATES WalkingState;

    public enum JUMPINGSTATES
    {
        JUMPING,
        FALLING,
        STANDING
    }

    private JUMPINGSTATES JumpingState;

    public readonly float MAXVELX = 3f;
    public readonly float ACCELX = 0.5f;
    public readonly float DEACCELX = 0.25f;

    public readonly float GRAVACCEL = 0.5f;
    public readonly float MAXGRAV = 7.5f;
    public readonly float JUMPACCELY = 1.5f;
    public readonly float MAXJUMPVEL = 7.5f;

    private readonly Vector2 PositionOnScreen;

    public Player(Bounds2 bounds, Texture texture) : base(bounds, texture)
    {
        PositionOnScreen = bounds.Position;
        JumpingState = JUMPINGSTATES.FALLING;
    }

    public override void Input()
    {
        if(Engine.GetKeyHeld(Key.A))
        {
            WalkingState = WALKINGSTATES.LEFT;
        }
        if(Engine.GetKeyHeld(Key.D))
        {
            WalkingState = WALKINGSTATES.RIGHT;
        }
        if(Engine.GetKeyHeld(Key.A) == Engine.GetKeyHeld(Key.D))
        {
            WalkingState = WALKINGSTATES.STANDING;
        }

        if(Engine.GetKeyHeld(Key.Space))
        {
            if(JumpingState == JUMPINGSTATES.STANDING)
            {
                JumpingState = JUMPINGSTATES.JUMPING;
            }
        }

        switch (WalkingState)
        {
            case WALKINGSTATES.LEFT:
                if (velocity.X - ACCELX >= MAXVELX)
                {
                    velocity.X -= ACCELX;
                }
                else
                {
                    velocity.X = -MAXVELX;
                }
                break;
            case WALKINGSTATES.RIGHT:
                if (velocity.X + ACCELX <= MAXVELX)
                {
                    velocity.X += ACCELX;
                }
                else
                {
                    velocity.X = MAXVELX;
                }
                break;
            case WALKINGSTATES.STANDING:
                if (velocity.X > 0)
                {
                    if (velocity.X - DEACCELX >= 0)
                    {
                        velocity.X -= DEACCELX;
                    }
                    else
                    {
                        velocity.X = 0;
                    }
                }
                else if (velocity.X < 0)
                {
                    if (velocity.X + DEACCELX <= 0)
                    {
                        velocity.X += DEACCELX;
                    }
                    else
                    {
                        velocity.X = 0;
                    }
                }
                break;
        }

        switch (JumpingState)
        {
            case JUMPINGSTATES.JUMPING:
                if (velocity.Y == -MAXJUMPVEL)
                {
                    JumpingState = JUMPINGSTATES.FALLING;
                }
                if (velocity.Y - JUMPACCELY >= MAXJUMPVEL)
                {
                    velocity.Y -= JUMPACCELY;
                }
                else
                {
                    velocity.Y = -MAXJUMPVEL;
                }
                break;
            case JUMPINGSTATES.FALLING:
                if (velocity.Y + GRAVACCEL <= MAXGRAV)
                {
                    velocity.Y += GRAVACCEL;
                }
                else
                {
                    velocity.Y = GRAVACCEL;
                }
                break;
            case JUMPINGSTATES.STANDING:
                velocity.Y = 0;
                break;
        }
    }

    public override void Update()
    {
        bounds.Position += velocity;
        
    }

    public void Render()
    {
        Engine.DrawTexture(texture, bounds.Position);
    }

    public override void Render(Vector2 offset)
    {

    }

    public Vector2 GetPositionOnScreen()
    {
        return PositionOnScreen;
    }

    public WALKINGSTATES GetWalkingState()
    {
        return WalkingState;
    }

    public JUMPINGSTATES GetJumpingState()
    {
        return JumpingState;
    }

    public void SetWalkingState(WALKINGSTATES state)
    {
        WalkingState = state;
    }

    public void SetJumpingState(JUMPINGSTATES state)
    {
        JumpingState = state;
    }
}

