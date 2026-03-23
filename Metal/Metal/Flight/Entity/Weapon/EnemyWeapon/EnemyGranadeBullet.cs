using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using static Entity;
using static System.Net.Mime.MediaTypeNames;

public class EnemyGranadeBullet : BulletEntity
{
    public EnemyGranadeBullet(GameScene scene, Point point, Point aim) : base(scene, point, (1, 0), 5, 5)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Ground | EntityType.Player;

        _useGravity = true;

        _life = 10f;
        _bulletSpeed = 80f;
        _gravity = 240f;
        Damage = 100;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;

        Velocity = aim.Normalize() * _bulletSpeed;
    }


    public override void CollisionToStatic()
    {
        Scene.AddGameObject(new HitEffect(Scene, Position));
        Destroy();
    }
    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
        Scene.AddGameObject(new HitEffect(Scene, Position));
        Destroy();
    }



    private string[] _idelPixels =
    {
        "  DD ",
        " DBRD",
        "DDRBD",
        "DDDD ",
        " DD  ",
    };
}