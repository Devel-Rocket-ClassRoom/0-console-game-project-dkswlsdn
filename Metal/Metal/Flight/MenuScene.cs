using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class MenuScene : Scene
{
    public event GameAction ToTitleRequest;
    public event GameAction GameStartRequest;


    public override void Load()
    {
    }

    public override void Unload()
    {
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            GameStartRequest?.Invoke();
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(0, 0, ShottingGame.k_Width, ShottingGame.k_Height);
        buffer.DrawBox(0, 5, 160, 30);
    }
}