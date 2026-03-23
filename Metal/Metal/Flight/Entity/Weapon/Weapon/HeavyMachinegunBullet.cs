using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegunBullet : BulletEntity
{
    private static int currentBulletCount = 0;

    private Point[] _spreadPattern = { (0.342f, 0.94f), (0.643f, 0.766f), (0.866f, 0.5f), (0.985f, 0.174f) };
    //private Point[] _spreadPattern = { (0.985f, 0.174f), (0.866f, 0.5f), (0.643f, 0.766f), (0.342f, 0.94f) };
    private float[] _bulletPattern = { 0.02f, -0.06f, 0.06f, -0.02f };
    private int _bulletCount;
    private Point _previous;

    private bool _isRasing = false;
    private bool _isLowering = false;


    public HeavyMachinegunBullet(GameScene scene, Point point, Point aim, int count, Point previous) 
        : base(scene, point, aim, 6, 6)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Enemy | EntityType.Ground;

        _previous = previous;
        _isRasing = previous.X != 0 && aim.Y != 0;
        _isLowering = previous.Y != 0 && aim.X != 0;

        _life = 1f;
        _bulletSpeed = 150f;
        Damage = 10;

        _interval = 0.2f;

        _bulletCount = count;

        DicideDirection();

        currentBulletCount++;
    }



    private void DicideDirection()
    {
        if (_isRasing || _isLowering)
        {
            switch (_bulletCount)
            {
                //case 3: _currentPixels = _7080Pixels; break;
                //case 2: _currentPixels = _5060Pixels; break;
                //case 1: _currentPixels = _3040Pixels; break;
                //case 0: _currentPixels = _1020Pixels; break;
                case 0: _currentPixels = _7080Pixels; break;
                case 1: _currentPixels = _5060Pixels; break;
                case 2: _currentPixels = _3040Pixels; break;
                case 3: _currentPixels = _1020Pixels; break;
            }

            Velocity = _spreadPattern[_bulletCount].DirectionConverter(_previous, Direction, out _pixelReversed) * _bulletSpeed;
        }
        else
        {
            _currentPixels = _idelPixels;

            if (Direction.Y != 0)
            {
                Velocity = new Point(_bulletPattern[_bulletCount], Direction.Y) * _bulletSpeed;
                return;
            }

            Velocity = new Point(Direction.X, _bulletPattern[_bulletCount]) * _bulletSpeed;
        }   
    }



    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
        //buffer.WriteText(Position, _bulletCount.ToString());

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
        "GYYYYy  ",
        "GGY     ",
    };

    private string[] _3040Pixels =
    {
        "      yy",
        "     yyy",
        "    YYy ",
        "  YYYY  ",
        " YYY    ",
        "GGY     ",
        "GG      ",
    };

    private string[] _5060Pixels =
    {
        "     yy",
        "    yyy",
        "   YYy ",
        "   YY  ",
        "  YY   ",
        " YYY   ",
        "GGY    ",
        "GG     ",
    };

    private string[] _7080Pixels =
    {
        "  yy",
        "  yy",
        "  yy",
        " YYY",
        " YY ",
        " YY ",
        "GYY ",
        "GG  ",
    };
}