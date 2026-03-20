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

    public HeavyMachinegunBullet(Scene scene, Entity id, Point point, Point direction, int count, Point previous, bool isEnemy = false) : base(scene, id, point, direction)
    {
        _runningDirection = new Point(direction);
        RectAngle = new RectAngle(this, (-2, 0), (2, 4));

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
        if (_runningDirection.Y != 0)
        {
            Position += (_bulletPattern[_bulletCount], _runningDirection.Y * _bulletSpeed);
            return;
        }

        Position += (_runningDirection.X * _bulletSpeed, _bulletPattern[_bulletCount]);
    }

    private void GetNextAngle(int isClockwise)
    {
        NextBulletAngle = NextBulletAngle + 20 * isClockwise;
    }

    private string[] _idelPixels =
    {
        "yy",
        "yy",
        "yy",
        "yy",
        "YY",
        "YY",
        "YY",
        "YY",
        "GG",
    };
}