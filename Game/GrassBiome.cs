using System;
using System.Collections.Generic;

class GrassBiome
{
    private List<List<int>> Terrain;

    private const int AIR = 0,
                      DIRT = 1,
                      GRASS = 2;

    public static readonly int LEVELONEWIDTH = 100;

    private const int TILESIZE = 32;

    private readonly Bounds2 screen = new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y);

    public GrassBiome()
    {
        GenerateTerrain();
    }

    public void Update(Player Player)
    {
        if (Engine.GetKeyDown(Key.F))
        {
            GenerateTerrain();
        }
        for (int y = 0; y < Terrain.Count; y++)
        {
            for (int x = 0; x < Terrain[y].Count; x++)
            {

                int type = Terrain[y][x];
                Bounds2 bounds = new Bounds2(((x - LEVELONEWIDTH / 2) * TILESIZE) - Player.GetOffset(), y * TILESIZE, TILESIZE, TILESIZE);



                if (screen.Overlaps(bounds))
                {
                    switch (type)
                    {
                        case AIR:
                            Engine.DrawRectSolid(bounds, new Color(38, 198, 235));
                            //Engine.DrawRectEmpty(tileBounds, Color.Black);
                            break;
                        case DIRT:
                            Engine.DrawRectSolid(bounds, Color.Brown);
                            break;
                        case GRASS:
                            Engine.DrawRectSolid(bounds, Color.Green);
                            break;
                    }
                }
            }
        }
    }

    private void GenerateTerrain()
    {
        //Grass
        Noise.Seed = new Random().Next(0, 999999999);
        int flatness = 100;
        float[] noise = Noise.Calc1D(flatness * LEVELONEWIDTH, 0.1f);
        Terrain = new List<List<int>>((int)(Game.Resolution.Y / TILESIZE));
        for (int y = 0; y < (int)(Game.Resolution.Y / TILESIZE); y++)
        {
            Terrain.Add(new List<int>(LEVELONEWIDTH));
            for (int x = 0; x < LEVELONEWIDTH; x++)
            {
                int height = 0;
                for (int i = 0; i < flatness; i++)
                {
                    height += (int)noise[x + i];
                }
                height /= (int)(flatness * 5f);
                height -= 13;
                Console.WriteLine(height);
                if (y == height)
                {

                    Terrain[y].Add(GRASS);
                }
                else if (y < height)
                {
                    Terrain[y].Add(AIR);
                }
                else if (y > height)
                {
                    Terrain[y].Add(DIRT);
                }
            }
        }


        //Snow
        /*
        Noise.Seed = new Random().Next(0, 999999999);
        int flatness = 50;
        float[] noise = Noise.Calc1D(flatness * LEVELONEWIDTH, 0.1f);
        Terrain = new List<List<int>>((int)(Game.Resolution.Y / TILESIZE));
        for (int y = 0; y < (int)(Game.Resolution.Y / TILESIZE); y++)
        {
            Terrain.Add(new List<int>(LEVELONEWIDTH));
            for (int x = 0; x < LEVELONEWIDTH; x++)
            {
                int height = 0;
                for (int i = 0; i < flatness; i++)
                {
                    height += (int)noise[x + i];
                }
                height /= (flatness * 7);
                height -= 4;
                if (y == height)
                {

                    Terrain[y].Add(GRASS);
                }
                else if (y < height)
                {
                    Terrain[y].Add(AIR);
                }
                else if (y > height)
                {
                    Terrain[y].Add(DIRT);
                }
            }
        }
        */
        //Mountain
        /*
        Noise.Seed = new Random().Next(0, 999999999);
        int flatness = 15;
        float[] noise = Noise.Calc1D(flatness * LEVELONEWIDTH, 0.1f);
        Terrain = new List<List<int>>((int)(Game.Resolution.Y / TILESIZE));
        for (int y = 0; y < (int)(Game.Resolution.Y / TILESIZE); y++)
        {
            Terrain.Add(new List<int>(LEVELONEWIDTH));
            for (int x = 0; x < LEVELONEWIDTH; x++)
            {
                int height = 0;
                for (int i = 0; i < flatness; i++)
                {
                    height += (int)noise[x + i];
                }
                height /= (flatness * 7);
                height -= 8;
                if (y == height)
                {

                    Terrain[y].Add(GRASS);
                }
                else if (y < height)
                {
                    Terrain[y].Add(AIR);
                }
                else if (y > height)
                {
                    Terrain[y].Add(DIRT);
                }
            }
        }
        */
    }
}
