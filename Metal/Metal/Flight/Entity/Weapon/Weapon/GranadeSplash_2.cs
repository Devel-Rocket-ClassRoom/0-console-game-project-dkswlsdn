using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GranadeSplash_2 : BulletEntity
{
    public GranadeSplash_2(GameScene scene, Point point) : base(scene, point - (14, 0), (1, 0), 28, 14)
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
        "                    r      ",
        "                    r      ",
        "                    r      ",
        "        R           r      ",
        "        R           r      ",
        "        R           r      ",
        "   r    R           r      ",
        "   r    R        rrrrrrr   ",
        "   r RRRRRRR        r      ",
        "   r    R           r      ",
        " rrrrr  R           r   R  ",
        "   r    R           r   R  ",
        "   r    R           r  RRR ",
        "   r    R           r   R  ",
        "   r    R           r   R  ",
    };
}