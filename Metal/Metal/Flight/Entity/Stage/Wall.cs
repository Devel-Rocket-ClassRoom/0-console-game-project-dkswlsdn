using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;


public class Wall : Entity
{
    public Wall(Scene scene, Point point, int height, string name = "w") : base(scene, point)
    {
        if (Scene is GameScene g)
        {
            g.WallEntitiyList.Add(this);
        }

        RectAngle = new RectAngle(this, (1, height));
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