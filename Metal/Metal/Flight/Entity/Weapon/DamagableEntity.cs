using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class DamagableEntity : AttackEntity
{
    public DamagableEntity(Scene scene, Point point, int damage) : base(scene, point, damage)
    {
        Position = point;
        Range = 10;
        RectAngle = new RectAngle(this, ((-2 , -2), (2, 2)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y, Position.ToString());
    }
}