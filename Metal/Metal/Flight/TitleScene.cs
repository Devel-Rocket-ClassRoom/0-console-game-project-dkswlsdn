using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class TitleScene : Scene
{
    

    public override void Load()
    {
    }

    public override void Unload()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(0, 0, 80, 40);

        buffer.WriteTextCentered(10, "F L I G H T");
    }
}