using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class Player : CharacterEntity, IMoveable, IJumpable, IAttackable
{
    private RectAngle _standingGround = new RectAngle((0, 0), (0, 0));
    private bool _isLand = false;
    public bool IsLand { get { return _isLand; } set { _isLand = value; } }
    private bool _isRight = true;
    private Point _aim = (1, 0);
    private int _jumpForce = 0;
    private Point _direction;

    public float JumpCooldown { private get;  set; } = 0.1f;


    public Point NextPosition { get { return (Position.X, Position.Y - 6); } }
    public Point GroundChecker { get { return Position - (0, 1); } }
    public int JumpForce { get { if (_jumpForce < 0 && IsLand) _jumpForce = 0; return _jumpForce; } set { _jumpForce = value; } }



    private string[] pixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        " DDD ",
        " DCC ",
        " DCC ",
        "BBBBB",
        "BGGGB",
        "BGGGB",
        "DBBBD",
        " B B ",
        " B B ",
        " B B ",
        " D D "
    };

    public Player(Scene scene, Point point) : base(scene, point)
    {
        _direction = (1, 0);

        Health = 100;

        RectAngle = new RectAngle( this, (-2, 0), (2, 10));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawPlayer(buffer);
        //RectAngle.DrawRectAngle(buffer);

        buffer.WriteText((1, 2), $"{Position.X}, {Position.Y}");
        buffer.WriteText((1, 3), $"{RectAngle.Position.X}, {RectAngle.Position.Y}");

        buffer.WriteText((1, 4), $"HP : {Health}");
        buffer.WriteText((1, 5), $"ID : {ID}");
        buffer.WriteText((1, 6), $"isLand : {IsLand}");
    }

    public override void Update(float deltaTime)
    {
        IsLand = IsOnGround();

        if (IsLand)
        {
            JumpForce = 0;
            Jump(deltaTime);
        }
        else
        {
            Land();
        }

        Move(2);
        Aimming();
        Attack();
        RectAngle.Follow();
    }

    public void Move(int speed)
    {
        if (Input.IsKey(ConsoleKey.D))
        {
            _isRight = true;

            if (IsLand)
                _direction = (1, 0);
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _isRight = false;

            if (IsLand)
                _direction = (-1, 0);
        }
        else
        {
            if (IsLand)
                _direction = (0, 0);
        }
        
        Position += (speed * _direction.X, 0);
    }

    public void VirticalMove(int force)
    {
        Position += (0, force / 2);
    }

    public void Jump(float deltaTime)
    {
        if (JumpCooldown > 0)
        {
            JumpCooldown -= deltaTime;
            return;
        }

        if (Input.IsKey(ConsoleKey.Spacebar))
        {
            JumpForce = 10;
            IsLand = false;
        }
    }

    public void Land()
    {
        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.GroundEntitiyList.Count; i++)
            {
                if (JumpForce <= 0 && g.GroundEntitiyList[i].RectAngle.IsOverrap((Position.X, Position.Y + JumpForce), Position))
                {
                    JumpCooldown = 0.1f;
                    IsLand = true;
                    JumpForce = 0;
                    Position.Y = g.GroundEntitiyList[i].Position.Y + 1;
                    _standingGround = g.GroundEntitiyList[i].RectAngle;
                    return;
                }
            }
        }

        VirticalMove(JumpForce--);
    }

    public void DrawPlayer(ScreenBuffer buffer)
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                ConsoleColor color = ConsoleColor.Black;
                int n = _isRight ? 1 : -1;

                switch (pixels[j][i * n + 2])
                {
                    case 'C':
                        color = ConsoleColor.Cyan;
                        break;
                    case 'D':
                        color = ConsoleColor.DarkBlue;
                        break;
                    case 'B':
                        color = ConsoleColor.Black;
                        break;
                    case 'G':
                        color = ConsoleColor.DarkGray;
                        break;
                    default:
                        continue;
                }

                buffer.SetCell((Position + new Point(i, -j + 10)), color);
            }
        }
    }


    private bool IsOnGround()
    {
        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.GroundEntitiyList.Count; i++)
            {
                if (JumpForce <= 0 && g.GroundEntitiyList[i].RectAngle.IsOverrap((Position.X, Position.Y - 1), Position))
                {
                    return true;
                }
            }
        }

        return false;

    }


    public Point GetNextPosition(int lowerForce)
    {
        throw new NotImplementedException();
    }

    public void Aimming()
    {
        if (Input.IsKey(ConsoleKey.D))
        {
            _aim.X = 1;
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _aim.X = -1;
        }

        if (Input.IsKey(ConsoleKey.W))
        {
            _aim.Y = 1;
        }
        else if (Input.IsKey(ConsoleKey.S))
        {
            if (!_isLand)
            {
                _aim.Y = -1;
            }
        }
        else
        {
            _aim.Y = 0;
        }
    }

    public void Attack()
    {
        if (Input.IsKeyDown(ConsoleKey.LeftArrow))
        {
            Scene.AddGameObject(new HandgunBullet(Scene, this, Position + (0, 5), _aim));
        }

        if (Input.IsKeyDown(ConsoleKey.RightArrow))
        {
            Scene.AddGameObject(new ShotgunBullet(Scene, this, Position + (0, 5), _aim));
        }
    }
}