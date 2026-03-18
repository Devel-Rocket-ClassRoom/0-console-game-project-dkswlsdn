using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class ShottingGame : GameApp
{
    public static int k_Width = 320;
    public static int k_Height = 80;

    private readonly SceneManager<Scene> scene = new SceneManager<Scene>();


    public ShottingGame() : base(40, 20) { }

    public ShottingGame(int width, int height) : base(width, height)
    {
    }

   

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }

        scene.CurrentScene?.Update(deltaTime);
    }

    protected override void Draw()
    {
        scene.CurrentScene?.Draw(Buffer);
    }

    private void ChangeToTitle()
    {
        var title = new TitleScene();
        title.StartRequested += ChangeToStageMenu;
        scene.ChangeScene(title);
    }

    private void ChangeToStageMenu()
    {
        var menu = new MenuScene();
        menu.ToTitleRequest += ChangeToTitle;
        menu.GameStartRequest += ChangeToGame;
        scene.ChangeScene(menu);
    }

    private void ChangeToGame()
    {
        var menu = new GameScene();
        scene.ChangeScene(menu);
    }
}