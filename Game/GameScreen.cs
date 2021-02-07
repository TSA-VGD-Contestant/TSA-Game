using System;
using System.IO;

class GameScreen : Screen
{
    private int[][] LevelOneTerrain;

    private readonly int tAIR = 0,
                         tGRASS = 1,
                         tDIRT = 2,
                         tSTONE = 3,
                         tSNOW = 4,
                         tICE = 5;

    private readonly int bGRASS = 0,
                         bMOUNTAIN = 1,
                         bSnOW = 2;
    private int CURRENTLEVEL;
    private Button Pause;

    public GameScreen()
    {
        //Initializing pause button
        Texture tPause = Engine.LoadTexture("PauseNormal.png");
        Pause = new Button(new Bounds2(25, 25, 50, 50), tPause, tPause, tPause);

        CURRENTLEVEL = bGRASS;

        LevelOne();
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


        if(CURRENTLEVEL == bGRASS)
        {
            for (int y = 0; y < LevelOneTerrain.Length; y++)
            {
                for (int x = 0; x < LevelOneTerrain[y].Length; x++)
                {
                    int tile = LevelOneTerrain[y][x];
                    if (tile == tGRASS)
                    {
                        Engine.DrawRectSolid(new Bounds2(x * 32, y * 32, 32, 32), Color.Green);
                    }
                    if (tile == tDIRT)
                    {
                        Engine.DrawRectSolid(new Bounds2(x * 32, y * 32, 32, 32), Color.Brown);
                    }
                }
            }
        }
        
    }

    private void LoadLevel(int LEVEL)
    {

    }

    private void SetLevel(int LEVEL)
    {
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
}
