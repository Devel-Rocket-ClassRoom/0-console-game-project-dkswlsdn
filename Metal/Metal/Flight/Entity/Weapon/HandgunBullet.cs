using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity
{
    public HandgunBullet(Scene scene, Entity id, Point point, Point direction, bool isEnemy = false) : base(scene, id, point, direction)
    {
        RectAngle = new RectAngle(Position, (-2, 0), (2, 6));

        _life = isEnemy ? 3f : 1f;
        _bulletSpeed = isEnemy ? 2 : 6;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;
    }


    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }

    protected override void Go()
    {
        if (_runningDirection.Y != 0)
        {
            Position.Y += _runningDirection.Y * _bulletSpeed;
            return;
        }

        Position.X += _runningDirection.X * _bulletSpeed;
    }


    private string[] _idelPixels =
    {
        "B",
        "R",
        "Y",
        "Y",
    };
}

