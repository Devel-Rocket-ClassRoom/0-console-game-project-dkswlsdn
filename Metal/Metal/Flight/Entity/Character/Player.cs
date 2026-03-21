using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class Player : CharacterEntity, IMoveable, IJumpable
{
    public Weapon mainWeapon;
    public Weapon subWeapon;

    private float _attackCooldown = 0.05f;
    private float _currentCooldown = 0f;


    private int _moveSpeed = 2;

    private bool _isSit = false;


    private float _currentVelocity = 0;
    public float JumpCooldown { private get;  set; } = 0.1f;
    public bool IsLand { get; set; }
    public float VirtlcalVelocity
    {
        set
        {
            _currentVelocity = MathF.Sqrt(2f * _gravity * value);
        }
    }
    private float _gravity = 10;




    public Point ForwardPosition
    {
        get { return Position + ((RectAngle.Width / 2 + 0) * Direction.X, 0); }
    }
    public Point BackwardPosition
    {
        get { return Position - ((RectAngle.Width / 2 + 1) * Direction.X, 0); }
    }

    public (Point a, Point b) ForwardSide
    {
        get
        {
            return (Position + ((RectAngle.Width / 2 + 0) * Direction.X, -RectAngle.Height / 2),
                Position + ((RectAngle.Width / 2 + 2) * Direction.X, RectAngle.Height / 2));
        }
    }
    public (Point a, Point b) BackwardSide
    {
        get
        {
            return (Position - ((RectAngle.Width / 2 + 1) * Direction.X, -RectAngle.Height / 2),
                Position - ((RectAngle.Width / 2 + 1) * Direction.X, RectAngle.Height / 2));
        }
    }
    public Point GroundChecker { get { return Position - (0, RectAngle.Height / 2); } }
    



    private Point _input;
    private Motion motion = Motion.Idle;





    public Player(Scene scene, Point point) : base(scene, point)
    {
        mainWeapon = new Handgun(Scene, this);
        subWeapon = new HeavyMachinegun(Scene, false);

        mainWeapon.OwnerID = this;
        subWeapon.OwnerID = this;

        Scene.AddGameObject(mainWeapon);
        Scene.AddGameObject(subWeapon);

        Health = 100;

        RectAngle = new RectAngle(this, (5, 11));

        _currentPixels = _idlePixels;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position - (0, 1), Health.ToString());
        buffer.WriteText(Camera.Position + (20, 5), mainWeapon.Name);
        buffer.WriteText(Camera.Position + (20, 4), mainWeapon.Arms.ToString());
        buffer.WriteText(Camera.Position + (30, 5), subWeapon.Name);
        buffer.WriteText(Camera.Position + (30, 4), subWeapon.Arms.ToString());

        buffer.WriteText(Position + (0, -1), Aim.ToString());
        buffer.WriteText(Position + (0, -2), Direction.ToString());

        buffer.SetCell(GroundChecker, ConsoleColor.Green);
    }




    public override void Update(float deltaTime)
    {
        PlayerInput();

        Move();

        base.Update(deltaTime);

        IsLand = IsOnGround(deltaTime);

        if (IsLand)
        {
            VirtlcalVelocity = 0;
            Jump(deltaTime);
        }
        else
        {
            _currentVelocity -= _gravity * deltaTime;
            VirticalMove();
        }
        
        Aimming();
        CheckMainArms();
        Animation();
    }

    public void Move()
    {
        if (_input.X != 0) Direction = (_input.X, 0);
        _isSit = IsLand && Input.IsKey(ConsoleKey.S);

        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.WallEntitiyList.Count; i++)
            {
                if (g.WallEntitiyList[i].RectAngle.IsOverrap(ForwardSide.a, ForwardSide.b))
                {
                    return;
                }
            }
        }

        Position += (_moveSpeed * _input.X, 0);
    }

    public void VirticalMove()
    {
        Position += (0, _currentVelocity);
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
            VirtlcalVelocity = 1.5f;
            IsLand = false;
        }
    }


    private bool IsOnGround(float deltaTime)
    {
        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.GroundEntitiyList.Count; i++)
            {
                if (_currentVelocity <= 0 && g.GroundEntitiyList[i].RectAngle.IsOverrap((GroundChecker.X - 3, GroundChecker.Y + Math.Min(_currentVelocity - _gravity * deltaTime, -1)), GroundChecker + (3, 0)))
                {
                    _currentVelocity = g.GroundEntitiyList[i].Position.Y - GroundChecker.Y + 1;
                    VirticalMove();
                    return true;
                }
            }
        }

        return false;
    }

    

    public void Aimming()
    {
        if (_input.X == 0 && _input.Y == 0)
        {
            int n = CurrentIsRight ? 1 : -1;
            Aim = (n, 0);
        }
        else if (IsLand && _input.Y == -1)
        {
            int n = CurrentIsRight ? 1 : -1;
            Aim = (n, 0);
        }
        else if (_input.Y != 0)
        {
            Aim = (0, _input.Y);
        }
        else
        {
            Aim = _input;
        }
    }


    private void PlayerInput()
    {
        if (Input.IsKey(ConsoleKey.W))
        {
            _input.Y = 1;
        }
        else if (Input.IsKey(ConsoleKey.S))
        {
            _input.Y = -1;
        }
        else
        {
            _input.Y = 0;
        }
        
        if (Input.IsKey(ConsoleKey.D))
        {
            _input.X = 1;
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _input.X = -1;
        }
        else
        {
            _input.X = 0;
        }
    }

    public void Animation()
    {
        
    }


    public void CheckMainArms()
    {
        if (mainWeapon.Arms <= 0)
        {
            Scene.RemoveGameObject(mainWeapon);
            mainWeapon = new Handgun(Scene, this);
            Scene.AddGameObject(mainWeapon);
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

    private string[] _lookUpPixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        "    DCC    ",
        "    DCC    ",
        "    DDD    ",
        "   BBBBB   ",
        "   BGGGB   ",
        "   BGGGB   ",
        "   DBBBD   ",
        "    B B    ",
        "    B B    ",
        "    B B    ",
        "    D D    "
    };

    private string[] _lookDownPixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        "    DDD    ",
        "    CCC    ",
        "    CCC    ",
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