using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegunBullet : BulletEntity
{
    private static int NextBulletAngle = 10;

    private Point[] _spreadPattern = { (0.342f, 0.94f), (0.643f, 0.766f), (0.866f, 0.5f), (0.985f, 0.174f) };
    //private Point[] _spreadPattern = { (0.985f, 0.174f), (0.866f, 0.5f), (0.643f, 0.766f), (0.342f, 0.94f) };
    private float[] _bulletPattern = { 0.02f, -0.06f, 0.06f, 0.02f };
    private int _bulletCount;
    private Point _previous;

    private bool _isRasing = false;
    private bool _isLowering = false;
    private Point speed;

    public HeavyMachinegunBullet(Scene scene, CharacterEntity id, Point point, Point aim, int count, Point previous, bool isEnemy = false) 
        : base(scene, id, point + new Point(3, 0).DirectionConverter(id.Aim), aim)
    {
        RectAngle = new RectAngle(this, (4, 4));

        _previous = previous;
        _isRasing = previous.X != 0 && aim.Y != 0 && !(previous.Y == -1 && id.IsLand);
        _isLowering = previous.Y != 0 && aim.X != 0;

        _life = isEnemy ? 3f : 1f;
        _bulletSpeed = isEnemy ? 2 : 6;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _bulletCount = count;

        DicideDirection();
    }

    protected override void Go()
    {
        Position += speed * _bulletSpeed;
    }

    private void GetNextAngle(int isClockwise)
    {
        NextBulletAngle = NextBulletAngle + 20 * isClockwise;
    }

    private void DicideDirection()
    {
        if (_isRasing || _isLowering)
        {
            switch (_bulletCount)
            {
                case 0: _currentPixels = _7080Pixels; break;
                case 1: _currentPixels = _5060Pixels; break;
                case 2: _currentPixels = _3040Pixels; break;
                case 3: _currentPixels = _1020Pixels; break;
                //case 0: _currentPixels = _1020Pixels; break;
                //case 1: _currentPixels = _3040Pixels; break;
                //case 2: _currentPixels = _5060Pixels; break;
                //case 3: _currentPixels = _7080Pixels; break;
            }

            speed = _spreadPattern[_bulletCount].DirectionConverter(_previous, Direction, out _pixelReversed);
        }
        else
        {
            _currentPixels = _idelPixels;

            if (Direction.Y != 0)
            {
                speed = (_bulletPattern[_bulletCount], Direction.Y);
                return;
            }

            speed = (ownerId.Direction.X, _bulletPattern[_bulletCount]);
        }   
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
        buffer.WriteText(Camera.Position + (0, 20), _bulletCount.ToString());
        buffer.WriteText(Camera.Position + (0, 19), _isRasing.ToString());
        buffer.WriteText(Camera.Position + (0, 18), _isLowering.ToString());
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