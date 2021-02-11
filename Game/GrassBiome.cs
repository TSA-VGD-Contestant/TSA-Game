using System;
using System.Collections.Generic;

class GrassBiome
{
    private List<List<int>> Terrain;
    private int[] Heights;
    private bool[] Flowers;



    public const int LEVELONEWIDTH = 1000;

    public const int TILESIZE = 32;

    private readonly Bounds2 screen = new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y);

    private readonly Texture Flower = Engine.LoadTexture("Flower.png");

    public GrassBiome()
    {
        GenerateTerrain();
    }

    public void Update(float Offset)
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
                Bounds2 bounds = new Bounds2(((x - LEVELONEWIDTH / 2) * TILESIZE) - Offset, y * TILESIZE, TILESIZE, TILESIZE);
                


                if (screen.Overlaps(bounds))
                {
                    switch (type)
                    {
                        case GameScreen.AIR:
                            Engine.DrawRectSolid(bounds, new Color(38, 198, 235));
                            //Engine.DrawRectEmpty(tileBounds, Color.Black);
                            break;
                        case GameScreen.DIRT:
                            Engine.DrawRectSolid(bounds, Color.Brown);
                            break;
                        case GameScreen.GRASS:
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
                Engine.DrawTexture(Flower, new Vector2(((x - LEVELONEWIDTH / 2) * TILESIZE) - Offset, Heights[x] * TILESIZE - TILESIZE));
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
                    Terrain[y].Add(GameScreen.GRASS);
                }
                else if (y < height)
                {
                    Terrain[y].Add(GameScreen.AIR);
                }
                else if (y > height)
                {
                    Terrain[y].Add(GameScreen.DIRT);
                }
            }
        }
        

        Flowers = new bool[LEVELONEWIDTH];
        for(int x = 0; x < LEVELONEWIDTH; x++)
        {
            int num = (int)(rand.NextDouble() * 10);
            if (num == 1)
            {
                
                Flowers[x] = true;
            }
            else
            {
                Flowers[x] = false;
            }
        }
    }

    public List<List<int>> GetTiles()
    {
        return Terrain;
    }
}
