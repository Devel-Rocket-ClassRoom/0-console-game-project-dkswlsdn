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
        buffer.DrawBox((0, ShottingGame.k_Height), ShottingGame.k_Width / 2, ShottingGame.k_Height);
    }
}