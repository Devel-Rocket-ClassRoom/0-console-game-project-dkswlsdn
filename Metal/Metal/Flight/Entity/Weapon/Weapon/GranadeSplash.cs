using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GranadeSplash : BulletEntity
{
    public GranadeSplash(GameScene scene, Point point) : base(scene, point - (7, 0), (1, 0), 14, 28)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Enemy;

        _life = 0.5f;
        _bulletSpeed = 0f;
        Damage = 1;

        _currentPixels = _idelPixels;

        _isOnlyTarget = false;
    }

    protected override void ShotPositionAdjust(Point aim)
    {
        
    }

    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
    }

    public override void CollisionToStatic()
    {
    }



    private string[] _idelPixels =
    {
        "       R       ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "     RRWRR     ",
        " RRRRRWWWRRRRR ",
        "RWWWWWWWWWWWWWR",
        " RRRRRWWWRRRRR ",
        "     RRWRR     ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "      RWR      ",
        "       R       ",
    };
}