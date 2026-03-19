using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GroundEntity
{
    public Ground(Scene scene, Point point, int Width) : base(scene, point)
    {
        RectAngle = new RectAngle(this, ((point.X - Width / 2, 0), (point.X + Width / 2, 0)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y, Position.ToString());
    }

    public override void Update(float deltaTime)
    {
        RectAngle.Follow();
    }
}