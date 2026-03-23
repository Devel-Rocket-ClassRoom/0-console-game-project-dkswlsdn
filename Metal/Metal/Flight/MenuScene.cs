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
        //AddGameObject(new Ending(this, (60, 40)));
    }

    public override void Unload()
    {
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
    }
}