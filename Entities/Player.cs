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


    //Movement constants
    public readonly float MAXVELX = 3f;
    public readonly float ACCELX = 0.5f;
    public readonly float DEACCELX = 0.25f;

    public readonly float GRAVACCEL = 0.5f;
    public readonly float MAXGRAV = 7.5f;
    public readonly float JUMPACCELY = 1.5f;
    public readonly float MAXJUMPVEL = 7.5f;

    private readonly Vector2 PositionOnScreen;

    //Raygun
    private RayGun raygun;
    private bool raygunEquipped;
    private const uint MAXAMMO = 6;
    private uint ammo;
    private bool shooting;

    private List<Projectile> projectiles;

    private readonly Texture ammoIndicator = Engine.LoadTexture("Ammo.png");

    private readonly Sound Lasor = Engine.LoadSound("PlayerLasor.mp3");

    public Player(Bounds2 bounds, Texture texture) : base(bounds, texture)
    {
        PositionOnScreen = bounds.Position;
        JumpingState = JUMPINGSTATES.FALLING;

        raygun = new RayGun(Engine.LoadTexture("RayGun.png"), 10);
        raygunEquipped = false;

        ammo = 6;

        shooting = false;

        projectiles = new List<Projectile>();
    }

    public override void Input()
    {
        if(Engine.GetKeyDown(Key.Space))
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
        if (Engine.GetKeyDown(Key.R))
        {
            if(raygunEquipped)
            {
                ammo = MAXAMMO;
            }
        }

        if(Engine.GetKeyDown(Key.Q))
        {
            raygunEquipped = !raygunEquipped;
        }
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

        if(Engine.GetKeyHeld(Key.W))
        {
            if(JumpingState == JUMPINGSTATES.STANDING)
            {
                JumpingState = JUMPINGSTATES.JUMPING;
            }
        }
    }

    public override void Update()
    {
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
                    else if(velocity.X + DEACCELX <= 0)
                    {
                        velocity.X += DEACCELX;
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
        bounds.Position += velocity;

        if(shooting && raygunEquipped && ammo > 0)
        {
            ammo --;
            if(raygun.IsFlipped())
            {
                projectiles.Add(new Projectile(Engine.LoadTexture("Projectile.png"), new Vector2(bounds.Position.X, bounds.Position.Y + bounds.Size.Y / 2 - 10), (raygun.IsFlipped()) ? -1 : 1, 10));
            }
            else
            {
                projectiles.Add(new Projectile(Engine.LoadTexture("Projectile.png"), new Vector2(bounds.Position.X + bounds.Size.X, bounds.Position.Y + bounds.Size.Y / 2 - 10), (raygun.IsFlipped()) ? -1 : 1, 10));
            }
            Engine.PlaySound(Lasor);
        }
    }

    public void Render()
    {
        for (int i = 0; i < ammo; i++)
        {
            Engine.DrawTexture(ammoIndicator, new Vector2(Game.Resolution.X - 32 - (i * 50), Game.Resolution.Y - 32));
        }

        Engine.DrawTexture(texture, bounds.Position);

        if(raygunEquipped)
        {
            switch (WalkingState)
            {
                case WALKINGSTATES.RIGHT:
                    RaygunRight();
                    break;
                case WALKINGSTATES.LEFT:
                    RaygunLeft();
                    break;
                case WALKINGSTATES.STANDING:
                    if (raygun.IsFlipped())
                    {
                        RaygunLeft();
                    }
                    else
                    {
                        RaygunRight();
                    }
                    break;
            }
        }
        foreach (Projectile p in projectiles)
        {
            p.Render();
        }



        
    }

    private void RaygunLeft()
    {
        raygun.Render(new Vector2(bounds.Position.X - bounds.Size.X / 3 + 10, bounds.Position.Y + bounds.Size.Y / 4), TextureMirror.Horizontal);
    }

    private void RaygunRight()
    {
        raygun.Render(new Vector2((float)(bounds.Position.X + (0.75 * bounds.Size.X)), bounds.Position.Y + bounds.Size.Y / 4), TextureMirror.None);
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

