class Biome
{
    public static readonly int AIR = -1,
                               DIRT = 0,
                               GRASS = 1,
                               SAND = 2,
                               STONE = 3,
                               ICE = 4;

    public static readonly int TILESIZE = 32;

    private int[][] terrain;

    public Biome()
    {

    }

    public void Update()
    {
        for (int y = 0; y < terrain.Length; y++)
        {
            for (int x = 0; x < terrain[y].Length; x++)
            {
                int type = terrain[y][x];
                if (type == AIR)
                {

                }
                else if (type == DIRT)
                {
                    Engine.DrawRectSolid(new Bounds2(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE), Color.Brown);
                }
                else if (type == GRASS)
                {
                    Engine.DrawRectSolid(new Bounds2(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE), Color.Green);
                }
                else if (type == SAND)
                {
                    Engine.DrawRectSolid(new Bounds2(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE), Color.Tan);
                }
                else if (type == STONE)
                {
                    Engine.DrawRectSolid(new Bounds2(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE), Color.Gray);
                }
                else if (type == ICE)
                {
                    Engine.DrawRectSolid(new Bounds2(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE), Color.Blue);
                }
            }
        }
    }

    public void Initialize(int[][] terrain)
    {
        this.terrain = terrain;
    }
}
