using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(Scene scene, int id, Point point, Point direction) : base(scene, id, point, 20, 0, direction)
    {
        RectAngle = new RectAngle(this, ((0, -5), (15, 5)));
        _interval = 0.3f;
        _life = 0.2f;
        _isOnlyTarget = false;
    }
}
