using System;
using System.IO;

class GameScreen : Screen
{
    private Player player;
    private GrassBiome grass;

    public const int AIR = 0,
                  DIRT = 1,
                  GRASS = 2;

    public GameScreen()
    {
        player = new Player();
        grass = new GrassBiome();
    }
    public override void Update()
    {
        Engine.DrawRectSolid(new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y), Color.SkyBlue);

        player.Input(grass.GetTiles());

        player.Update();
        grass.Update(player.GetOffset());
        player.Render();
    }
}
