using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class DamagableEntity : AttackEntity
{
    public DamagableEntity(Scene scene, int id, int damage) : base(scene, id, damage)
    {
        Position = (200, 70);
        Range = 10;
        RectAngle = new RectAngle(Scene, this, ((4, -2), (-4, 2)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        
    }
}