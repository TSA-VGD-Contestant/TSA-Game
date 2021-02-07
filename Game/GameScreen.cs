using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GameScreen : Screen
{
    private Button Pause;
    
    public GameScreen()
    {
        Texture tPause = Engine.LoadTexture("PauseNormal.png");
        Pause = new Button(new Bounds2(25, 25, 50, 50), tPause, tPause, tPause);
    }

    public override void Update()
    {
        Engine.DrawRectSolid(new Bounds2(0, 0, Game.Resolution.X, Game.Resolution.Y), Color.Cyan);
        Pause.Update();
        if(Pause.IsClicked())
        {
            Game.SetGameScreen(Game.PAUSE);
        }
    }
}
