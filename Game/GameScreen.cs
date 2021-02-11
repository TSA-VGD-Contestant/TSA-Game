using System;
using System.IO;

class GameScreen : Screen
{
    private Player player;
    private GrassBiome grass;

    public GameScreen()
    {
        player = new Player();
        grass = new GrassBiome();
    }
    public override void Update()
    {
        grass.Update(player);
        player.Input();
        player.Update();
        player.Render();
        player.Render();
    }
}
