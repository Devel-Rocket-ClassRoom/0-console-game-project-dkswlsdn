using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class Player : CharacterEntity, IMoveable, IJumpable, IAttackable
{
    private float _attackCooldown = 0.05f;
    private float _currentCooldown = 0f;


    private bool _isLand = false;
    private int _moveSpeed = 2;

    public bool IsLand { get { return _isLand; } set { _isLand = value; } }
    public Point Aim = (1, 0);


    private int _jumpForce = 0;
    private Point _bulletPoint { get { return Position + (0, 5); } }

    public float JumpCooldown { private get;  set; } = 0.1f;


    public Point NextPosition { get { return (Position.X, Position.Y - 6); } }
    public Point GroundChecker { get { return Position - (0, 1); } }
    public int JumpForce { get { if (_jumpForce < 0 && IsLand) _jumpForce = 0; return _jumpForce; } set { _jumpForce = value; } }


    public Player(Scene scene, Point point) : base(scene, point)
    {
        Health = 100;

        RectAngle = new RectAngle( this, (-2, 0), (2, 10));

        _currentPixels = _idlePixels;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position - (0, 1), Health.ToString());
    }

    public override void Update(float deltaTime)
    {
        Move();
        base.Update(deltaTime);

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
        
        Aimming();
        Attack(deltaTime);
    }

    public void Move()
    {
        if (Input.IsKey(ConsoleKey.D))
        {
            _runningDirection = (1, 0);
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _runningDirection = (-1, 0);
        }
        else
        {
            if (IsLand)
            {
                _runningDirection = (0, 0);
            }
        }

        
        Position += (_moveSpeed * _runningDirection.X, 0);
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
                    return;
                }
            }
        }

        VirticalMove(JumpForce--);
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

    

    public void Aimming()
    {
        if (Input.IsKey(ConsoleKey.W))
        {
            Aim = (0, 1);
            return;
        }
        else if (Input.IsKey(ConsoleKey.S))
        {
            if (!_isLand)
            {
                Aim = (0, -1);
                return;
            }
        }
        else
        {
            if (Input.IsKey(ConsoleKey.D))
            {
                Aim = (1, 0);
            }
            else if (Input.IsKey(ConsoleKey.A))
            {
                Aim = (-1, 0);
            }
            else
            {
                if (_isLand)
                {
                    if (_currentIsRight)
                    {
                        Aim = (1, 0);
                    }
                    else
                    {
                        Aim = (-1, 0);
                    }
                }
            }
        }
    }

    public void Attack(float deltaTime)
    {
        if (_currentCooldown <= 0)
        {
            if (Input.IsKeyDown(ConsoleKey.LeftArrow))
            {
                Scene.AddGameObject(new HandgunBullet(Scene, this, _bulletPoint, Aim));
                _currentCooldown = _attackCooldown;
            }

            if (Input.IsKeyDown(ConsoleKey.RightArrow))
            {
                Scene.AddGameObject(new ShotgunBullet(Scene, this, _bulletPoint, Aim));
                _currentCooldown = _attackCooldown;
            }
        }
        else
        {
            _currentCooldown -= deltaTime;
        }
    }



    private enum State
    {
        Idle,
        SitDown,
        Jump,
    }

    private string[] _idlePixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        "    DDD    ",
        "    DCC    ",
        "    DCC    ",
        "   BBBBB   ",
        "   BGGGB   ",
        "   BGGGB   ",
        "   DBBBD   ",
        "    B B    ",
        "    B B    ",
        "    B B    ",
        "    D D    "
    };

    private string[] _sitPixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        "           ",
        "           ",
        "           ",
        "     DDD   ",
        "     DCC   ",
        "     DCC   ",
        "   BBBBBB  ",
        "   BBGGGG  ",
        "   BBBBBB  ",
        "   B    B  ",
        "DBBB    D  ",

    };


}