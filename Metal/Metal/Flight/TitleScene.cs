using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class TitleScene : Scene
{
    public event GameAction StartRequested;


    public override void Load()
    {
        AddGameObject(new Title(this, (50, 30)));
    }

    public override void Unload()
    {
        ClearGameObjects();
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
        DrawGameObjects(buffer);
    }
}