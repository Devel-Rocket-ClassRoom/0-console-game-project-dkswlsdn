using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class Player : CharacterEntity
{
    public Weapon mainWeapon;
    public Weapon subWeapon;



    private int _moveSpeed = 2;

    private bool _isSit = false;


    public float JumpCooldown { private get;  set; } = 0.1f;
    

    
    
    



    private Point _input;
    private HigherState _state = HigherState.Idle;





    public Player(Scene scene, Point point) : base(scene, point)
    {
        Team = 1;

        mainWeapon = new Handgun(Scene, this);
        subWeapon = new HeavyMachinegun(Scene, false);
        subWeapon.Arms = 20000;

        mainWeapon.OwnerID = this;
        subWeapon.OwnerID = this;

        Scene.AddGameObject(mainWeapon);
        Scene.AddGameObject(subWeapon);

        Health = 100;

        RectAngle = new RectAngle(this, (5, 11));

        _currentPixels = _idlePixels;
        _useGravity = true;
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

        if (IsLand) Jump(deltaTime);

        base.Update(deltaTime);

        
        Aimming();
        CheckMainArms();
        //CheckTransitions();
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

    

    public void Jump(float deltaTime)
    {
        if (JumpCooldown > 0)
        {
            JumpCooldown -= deltaTime;
            return;
        }

        if (Input.IsKey(ConsoleKey.Spacebar))
        {
            VirticalVelocity = 1f;
            IsLand = false;
        }
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
    public void CheckMainArms()
    {
        if (mainWeapon.Arms <= 0)
        {
            Scene.RemoveGameObject(mainWeapon);
            mainWeapon = new Handgun(Scene, this);
            Scene.AddGameObject(mainWeapon);
        }
    }


    public void CheckTransitions()
    {
        switch (_state)
        {
            case HigherState.Idle:
                //인풋이 위쪽이라면 LookUp상태
                //인풋이 아래쪽이라면 SitDown상태
                //인풋이 양옆이라면 Walk상태
                //점프를 했다면 Jump상태
                //총을 쐈다면 Shot상태
                break;
            case HigherState.LookUp:
                //인풋이 양옆이라면 LookUpWalk상태
                //점프를 했다면 JumpLookup상태
                //총을 쐈다면
                break;

        }
    }


    protected string[] GetPixels(string[] higher, string[] lower)
    {
        string[] union = new string[11];

        for (int i = 0; i < 7; i++) union[i] = higher[i];
        for (int i = 7; i < lower.Length; i++) union[i] = lower [i];

        return union;
    }

    public enum HigherState
    {
        Idle,
        LookUp,
        LookDown,
        Shot
    }
    public enum LowerState
    {
        Idle,
        Run,
        Jump
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