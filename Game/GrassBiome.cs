using System;
using System.Collections.Generic;

class GrassBiome
{
    private List<List<int>> Terrain;
    private int[] Heights;
    private bool[] Flowers;

    private const int AIR = 0,
                      DIRT = 1,
                      GRASS = 2;

    public static readonly int LEVELONEWIDTH = 100;

    private const int TILESIZE = 32;

    private readonly Bounds2 screen = new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y);

    private readonly Texture Flower = Engine.LoadTexture("Flower.png");

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

        for(int x = 0; x < Flowers.Length; x++)
        {
            if(Flowers[x] == true)
            {
                Engine.DrawTexture(Flower, new Vector2(((x - LEVELONEWIDTH / 2) * TILESIZE) - Player.GetOffset(), Heights[x] * 32 - 32));
            }
        }
    }

    private void GenerateTerrain()
    {
        Random rand = new Random();
        //Grass
        Noise.Seed = rand.Next(0, 999999999);
        int flatness = 100;
        float[] noise = Noise.Calc1D(flatness * LEVELONEWIDTH, 0.1f);
        Terrain = new List<List<int>>((int)(Game.Resolution.Y / TILESIZE));
        Heights = new int[LEVELONEWIDTH];
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
                
                if (y == height)
                {
                    Heights[x] = height;
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

        Flowers = new bool[LEVELONEWIDTH];
        for(int x = 0; x < LEVELONEWIDTH; x++)
        {
            int num = (int)(rand.NextDouble() * 10);
            Console.WriteLine(num);
            if (num == 1)
            {
                
                Flowers[x] = true;
            }
            else
            {
                Flowers[x] = false;
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
