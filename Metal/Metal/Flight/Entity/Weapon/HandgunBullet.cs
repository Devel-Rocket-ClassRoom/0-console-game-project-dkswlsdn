using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity
{
    public HandgunBullet(Scene scene, CharacterEntity id, Point point, Point aim, bool isEnemy = false) : base(scene, id, point, aim)
    {
        RectAngle = new RectAngle(this, (5, 7));

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
        if (Direction.Y != 0)
        {
            Position += (0, Direction.Y * _bulletSpeed);
            return;
        }

        Position += Direction * _bulletSpeed;
    }


    private string[] _idelPixels =
    {
        "YYRB"
    };
}

