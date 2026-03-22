using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HandgunBullet : BulletEntity
{
    public HandgunBullet(Scene scene, Point point, Point aim) : base(scene, point, aim, 5, 3)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Enemy | EntityType.Ground;

        _life = 1f;
        _bulletSpeed =  150f;
        Damage = 10;

        _currentPixels = _idelPixels;

        Velocity = Direction * _bulletSpeed;
    }


    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }


    private string[] _idelPixels =
    {
        "YYRB"
    };

    
}

