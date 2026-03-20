using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class TitleScene : Scene
{
    public event GameAction StartRequested;


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
            StartRequested?.Invoke();
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox((0, 0), ShottingGame.k_Width, ShottingGame.k_Height);

        buffer.WriteTextCentered(10, "F L I G H T");
    }
}