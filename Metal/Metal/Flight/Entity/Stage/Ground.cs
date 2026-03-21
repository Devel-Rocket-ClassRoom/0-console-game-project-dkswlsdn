using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GroundEntity
{
    public Ground(Scene scene, Point point, int width, string name = "g") : base(scene, point)
    {
        if (Scene is GameScene g)
        {
            g.GroundEntitiyList.Add(this);
            g.GroundEntitiyList.Sort((a, b) => b.Position.Y.CompareTo(a.Position.Y));
        }

        RectAngle = new RectAngle(this, (width, 1));
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