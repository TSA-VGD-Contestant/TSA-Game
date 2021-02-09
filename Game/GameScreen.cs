using System;
using System.IO;

class GameScreen : Screen
{
    private int[][] LevelOneTerrain, LevelTwoTerrain, LevelThreeTerrain;

    private Player player;

    private readonly int tAIR = 0,
                         tGRASS = 1,
                         tDIRT = 2,
                         tSTONE = 3,
                         tSNOW = 4,
                         tICE = 5;

    private readonly int bGRASS = 0,
                         bMOUNTAIN = 1,
                         bSNOW = 2;
    private int CURRENTLEVEL;
    private Button Pause;



    public GameScreen()
    {
        //Initializing pause button
        Texture tPause = Engine.LoadTexture("PauseNormal.png");
        Pause = new Button(new Bounds2(25, 25, 32, 32), tPause, tPause, tPause);

        LevelOne();
        LevelThree();

        SetLevel(bGRASS);

        player = new Player(new Bounds2(100, 100, 64, 64), Engine.LoadTexture("PlayerForward.png"));
    }



    public override void Update()
    {
        //Sky
        Engine.DrawRectSolid(new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y), Color.Cyan);

        //Pause Button
        Pause.Update();

        if (Pause.IsClicked())
        {
            Game.SetGameScreen(Game.PAUSE);
        }

        player.Input();
        player.Update();

        if (CURRENTLEVEL == bGRASS)
        {
            for (int y = 0; y < LevelOneTerrain.Length; y++)
            {
                for (int x = 0; x < LevelOneTerrain[y].Length; x++)
                {
                    int tile = LevelOneTerrain[y][x];
                    Bounds2 bounds = new Bounds2(x * 32, y * 32, 32, 32);
                    DrawTile(tile, bounds);
                    if(tile != tAIR)
                    {
                        if (player.Hits(bounds))
                        {
                            if(player.GetJumpingState() == Player.JUMPINGSTATES.FALLING)
                            {
                                player.SetPosition(new Vector2(player.GetPosition().X, (y * 32) - player.GetBounds().Size.Y));
                                player.SetJumpingState(Player.JUMPINGSTATES.STANDING);
                            }
                        }
                    }

                }
            }
        }
        else if(CURRENTLEVEL == bMOUNTAIN)
        {
            for (int y = 0; y < LevelTwoTerrain.Length; y++)
            {
                for (int x = 0; x < LevelTwoTerrain[y].Length; x++)
                {
                    int tile = LevelTwoTerrain[y][x];
                    Bounds2 bounds = new Bounds2(x * 32, y * 32, 32, 32);
                    DrawTile(tile, bounds);
                }
            }
        }
        else if (CURRENTLEVEL == bSNOW)
        {
            for (int y = 0; y < LevelThreeTerrain.Length; y++)
            {
                for (int x = 0; x < LevelThreeTerrain[y].Length; x++)
                {
                    int tile = LevelThreeTerrain[y][x];
                    Bounds2 bounds = new Bounds2(x * 32, y * 32, 32, 32);
                    DrawTile(tile, bounds);
                }
            }
        }

        player.Render();

    }

    private void DrawTile(int tile, Bounds2 bounds)
    {
        if (tile == tGRASS)
        {
            Engine.DrawRectSolid(bounds, Color.Green);
        }
        else if (tile == tDIRT)
        {
            Engine.DrawRectSolid(bounds, Color.Brown);
        }
        else if (tile == tSTONE)
        {
            Engine.DrawRectSolid(bounds, Color.Gray);
        }
        else if (tile == tSNOW)
        {
            Engine.DrawRectSolid(bounds, Color.AliceBlue);
        }
        else if (tile == tICE)
        {
            Engine.DrawRectSolid(bounds, Color.CadetBlue);
        }
    }

    private void LoadLevel(int LEVEL)
    {
        
    }

    private void SetLevel(int LEVEL)
    {
        CURRENTLEVEL = LEVEL;
        LoadLevel(LEVEL);
    }

    private void LevelOne()
    {
        int tx = (int)(Game.Resolution.X / 32);
        int ty = (int)(Game.Resolution.Y / 32);
        LevelOneTerrain = new int[ty][];

        for(int i = 0; i < LevelOneTerrain.Length; i++)
        {
            LevelOneTerrain[i] = new int[tx];
        }

        int height = 5;

        //Set air
        for(int y = 0; y < ty - height; y++)
        {
            for(int x = 0; x < tx; x++)
            {
                LevelOneTerrain[y][x] = tAIR;
            }
        }

        //Set grass
        for (int x = 0; x < tx; x++)
        {
            LevelOneTerrain[ty - height][x] = tGRASS;
        }

        //Set dirt
        for (int y = ty - (height - 1); y < ty; y++)
        {
            for (int x = 0; x < tx; x++)
            {
                LevelOneTerrain[y][x] = tDIRT;
            }
        }


    }


    private void LevelTwo()
    {
        int tx = (int)(Game.Resolution.X / 32);
        int ty = (int)(Game.Resolution.Y / 32);
        LevelThreeTerrain = new int[ty][];

        for (int i = 0; i < LevelThreeTerrain.Length; i++)
        {
            LevelThreeTerrain[i] = new int[tx];
        }

        int height = 5;

        //Set air
        for (int y = 0; y < ty - height; y++)
        {
            for (int x = 0; x < tx; x++)
            {
                LevelThreeTerrain[y][x] = tAIR;
            }
        }

        //Set grass
        for (int x = 0; x < tx; x++)
        {
            LevelThreeTerrain[ty - height][x] = tSNOW;
        }

        //Set dirt
        for (int y = ty - (height - 1); y < ty; y++)
        {
            for (int x = 0; x < tx; x++)
            {
                LevelThreeTerrain[y][x] = tICE;
            }
        }


    }

    private void LevelThree()
    {
        int tx = (int)(Game.Resolution.X / 32);
        int ty = (int)(Game.Resolution.Y / 32);
        LevelThreeTerrain = new int[ty][];

        for (int i = 0; i < LevelThreeTerrain.Length; i++)
        {
            LevelThreeTerrain[i] = new int[tx];
        }

        int height = 5;

        //Set air
        for (int y = 0; y < ty - height; y++)
        {
            for (int x = 0; x < tx; x++)
            {
                LevelThreeTerrain[y][x] = tAIR;
            }
        }

        //Set grass
        for (int x = 0; x < tx; x++)
        {
            LevelThreeTerrain[ty - height][x] = tSNOW;
        }

        //Set dirt
        for (int y = ty - (height - 1); y < ty; y++)
        {
            for (int x = 0; x < tx; x++)
            {
                LevelThreeTerrain[y][x] = tICE;
            }
        }


    }
}
