using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegunBullet : BulletEntity
{
    private static int NextBulletAngle = 10;

    private (float x, float y)[] _spreadPattern = { (0.985f, 0.174f), (0.94f, 0.342f), (0.866f, 0.5f), (0.766f, 0.643f), (0.643f, 0.766f), (0.5f, 0.866f), (0.342f, 0.94f), (0.174f, 0.985f) };
    private float[] _bulletPattern = { 0.1f, -0.3f, 0.3f, 0.1f };
    private int _bulletCount;
    private Point _previous;

    public HeavyMachinegunBullet(Scene scene, Entity id, Point point, Point aim, int count, Point previous, bool isEnemy = false) 
        : base(scene, id, point + Point.DirectionConverter((5, 0), aim), aim)
    {
        RectAngle = new RectAngle(this, (4, 4));

        _life = isEnemy ? 3f : 1f;
        _bulletSpeed = isEnemy ? 2 : 6;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;

        _bulletCount = count;
    }

    protected override void Go()
    {
        if (Direction.Y != 0)
        {
            Position += (_bulletPattern[_bulletCount], Direction.Y * _bulletSpeed);
            return;
        }

        Position += (Direction.X * _bulletSpeed, _bulletPattern[_bulletCount]);
    }

    private void GetNextAngle(int isClockwise)
    {
        NextBulletAngle = NextBulletAngle + 20 * isClockwise;
    }

    private string[] _idelPixels =
    {
        "GYYYYYYyyy",
        "GYYYYYYyyy",
    };

    
    private string[] _1020Pixels =
    {
        "     yyy",
        "  YYYyyy",
        "GYYYY   ",
        "GY      ",
    };

    private string[] _3040Pixels =
    {
        "      yy",
        "     yy ",
        "    Yy  ",
        "   YY   ",
        "  YY    ",
        " YY     ",
        "GG      ",
    };

    private string[] _5060Pixels =
    {
        "      y",
        "     yy",
        "    Yy ",
        "   YY  ",
        "  YY   ",
        "  Y    ",
        "GG     ",
        "G      ",
    };

    private string[] _7080Pixels =
    {
        "  yy",
        "  yy",
        "  yy",
        "  YY",
        " YY ",
        " YY ",
        " YY ",
        "GG  ",
    };
}