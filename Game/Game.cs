using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "TSA Game";
    public static readonly Vector2 Resolution = new Vector2(1024, 800);

    public static readonly int MENU = 0;
    public static readonly int GAME = 1;
    public static readonly int PAUSE = 2;

    private static MenuScreen menu;
    private static GameScreen game;
    private static PauseScreen pause;

    private static Screen current;

    public Game()
    {
        menu = new MenuScreen();
        game = new GameScreen();
        pause = new PauseScreen();

        current = menu;
    }

    public void Update()
    {
        current.Update();
    }

    public static void SetGameScreen(int SCREEN)
    {
        if (SCREEN == MENU)
        {
            current = menu;
        }
        else if (SCREEN == GAME)
        {
            current = game;
        }
        else if(SCREEN == PAUSE)
        {
            current = pause;
        }
    }
}
