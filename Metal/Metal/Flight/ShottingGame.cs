using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class ShottingGame : GameApp
{
    private readonly SceneManager<Scene> scene = new SceneManager<Scene>();


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
        scene.ChangeScene(title);
    }
}