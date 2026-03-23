using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class CannonBullet : BulletEntity
{
    Point _runningDirection;

    public CannonBullet(GameScene scene, Point point, Point aim) : base(scene, point, (1, 0), 4, 4)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Player;

        _life = 10f;
        _bulletSpeed = 30f;
        Damage = 1;


        _currentPixels = _idelPixels;

        Velocity = aim.Normalize() * _bulletSpeed;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

    }


    private string[] _idelPixels =
    {
        " DD ",
        "DRBD",
        "DDRD",
        " DD ",
    };

    
}