using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GroundEntity
{
    public Ground(Scene scene, Point point, int Width, string name = "g") : base(scene, point)
    {
        RectAngle = new RectAngle(this, (Width, 1));
        Name = name;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
        //buffer.WriteText(Position, Position.ToString());
    }

    public override void Update(float deltaTime)
    {
        RectAngle.Follow();
    }
}