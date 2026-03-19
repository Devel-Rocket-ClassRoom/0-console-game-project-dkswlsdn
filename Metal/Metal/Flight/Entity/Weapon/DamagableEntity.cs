using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class DamagableEntity : AttackEntity
{
    public DamagableEntity(Scene scene, Point point, int damage) : base(scene, point, damage)
    {
        Position = (200, 70);
        Range = 10;
        RectAngle = new RectAngle(this, ((4, -2), (-4, 2)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        
    }
}