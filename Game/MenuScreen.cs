using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MenuScreen : Screen
{
    private Button Play;

    public MenuScreen()
    {
        Play = new Button(new Bounds2(50, 50, 200, 150), Engine.LoadTexture("Normal.png"), Engine.LoadTexture("Hovered.png"), Engine.LoadTexture("Pressed.png")); 
    }

    public override void Update()
    {
        Play.Update();
        if(Play.IsClicked())
        {
            Game.SetGameScreen(Game.GAME);
        }
    }
}
