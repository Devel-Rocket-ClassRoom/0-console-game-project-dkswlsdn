using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity
{
    public HandgunBullet(Scene scene, Entity id, Point point, Point direction) : base(scene, id, point, 1, 6, direction)
    {
        RectAngle = new RectAngle(point, (0, 0), (2, 0));
        RectAngle.SpinRect(direction);

        _isOnlyTarget = true;
        _interval = 0.2f;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }

    protected override void Go()
    {
        if (_direction.y != 0)
        {
            Position.Y += _direction.y * _bulletSpeed;
            return;
        }

        Position.X += _direction.x * _bulletSpeed;
    }
}