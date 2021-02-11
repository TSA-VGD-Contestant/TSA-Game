using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        InitTerrain();
    }

    public void Update(Player Player)
    {
        for (int y = 0; y < Terrain.Count; y++)
        {
            for (int x = 0; x < Terrain[y].Count; x++)
            {
                
                int type = Terrain[y][x];
                Bounds2 tileBounds = new Bounds2(((x - LEVELONEWIDTH / 2) * TILESIZE) - Player.GetOffset(), y * TILESIZE, TILESIZE, TILESIZE);
                
                if(screen.Overlaps(tileBounds))
                {
                    switch (type)
                    {
                        case AIR:
                            Engine.DrawRectSolid(tileBounds, Color.Blue);
                            Engine.DrawRectEmpty(tileBounds, Color.Black);
                            break;
                        case DIRT:
                            Engine.DrawRectSolid(tileBounds, Color.Brown);
                            Engine.DrawRectEmpty(tileBounds, Color.Black);
                            break;
                        case GRASS:
                            Engine.DrawRectSolid(tileBounds, Color.Green);
                            Engine.DrawRectEmpty(tileBounds, Color.Black);
                            break;
                    }
                }

            }
        }
    }

    private void InitTerrain()
    {
        Terrain = new List<List<int>>((int)(Game.Resolution.Y / TILESIZE));
        Terrain.Add(new List<int>());
        for(int y = 0; y < (int)(Game.Resolution.Y / TILESIZE); y++)
        {
            Terrain.Add(new List<int>(LEVELONEWIDTH));
            for(int x = 0; x < LEVELONEWIDTH; x++)
            {
                if (y <= 10)
                {
                    Terrain[y].Add(AIR);
                }
                else if (y <= 11)
                {
                    Terrain[y].Add(GRASS);
                }
                else
                {
                    Terrain[y].Add(DIRT);
                }
            }
        }
    }
}
