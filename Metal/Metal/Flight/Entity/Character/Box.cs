using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Box : CharacterEntity
{
    int a = 0;
    public Box(Scene scene, Point point) : base(scene, point)
    {
        Health = 100;

        RectAngle = new RectAngle(this, ((-2, 0), (2, 10)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y - 1, Health.ToString());
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y, a.ToString(), ConsoleColor.Red);
    }

    public override void Update(float deltaTime)
    {
        RectAngle.Follow();
    }

    protected override void temp()
    {
        a++;
    }
}