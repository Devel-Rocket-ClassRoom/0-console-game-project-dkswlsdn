using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity
{
    public HandgunBullet(Scene scene, int id, Point point, Point direction) : base(scene, id, point, 1, 4, direction)
    {
        RectAngle = new RectAngle(this, ((0, 0), (0, 0)));
        _isOnlyTarget = true;
        _interval = 0.2f;
    }
}