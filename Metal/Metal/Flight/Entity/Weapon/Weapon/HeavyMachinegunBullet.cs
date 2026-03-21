using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegunBullet : BulletEntity, IMoveable
{
    private static int currentBulletCount = 0;

    private Point[] _spreadPattern = { (0.342f, 0.94f), (0.643f, 0.766f), (0.866f, 0.5f), (0.985f, 0.174f) };
    //private Point[] _spreadPattern = { (0.985f, 0.174f), (0.866f, 0.5f), (0.643f, 0.766f), (0.342f, 0.94f) };
    private float[] _bulletPattern = { 0.02f, -0.06f, 0.06f, -0.02f };
    private int _bulletCount;
    private int _spreadingBulletCount;
    private Point _previous;

    private bool _isRasing = false;
    private bool _isLowering = false;
    private Point speed;


    public Point ForwardPosition
    { // 픽셀의 정면점
        get { return Position + new Point(Breadth / 2, 0).PointConverter(Direction); }
    }
    public Point BackwardPosition
    { // 픽셀이 판정보다 작을 때 픽셀의 뒷면에 판정의 뒷면을 붙임
        get { return Position - (((RectAngle.Width - Breadth) / 2 + 1) * Direction.X, 0); }
    }




    public HeavyMachinegunBullet(Scene scene, CharacterEntity id, Point point, Point aim, int count, Point previous, bool isEnemy = false) 
        : base(scene, id, point + new Point(3, 0).PointConverter(id.Aim), aim)
    {
        RectAngle = new RectAngle(this, (8, 4));

        _previous = previous;
        _isRasing = previous.X != 0 && aim.Y != 0;
        _isLowering = previous.Y != 0 && aim.X != 0;

        _life = isEnemy ? 3f : 1f;
        _bulletSpeed = isEnemy ? 2f : 6f;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _bulletCount = count;

        DicideDirection();

        currentBulletCount++;
    }

    public void Move()
    {
        Position += speed * _bulletSpeed;
    }

    public override void Update(float deltaTime)
    {
        Move();

        base.Update(deltaTime);

        RectAngle.Follow(Position);
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