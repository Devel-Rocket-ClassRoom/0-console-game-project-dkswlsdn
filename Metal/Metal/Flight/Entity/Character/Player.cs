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
    public float JumpCooldown { private get;  set; } = 0.1f;

    private float _speed = 30f;


    public override Point Position
    {
        get { return base.Position; }
        set
        {
            if (Velocity.X < 0 && value.X < Camera.LeftClamp) return;
            if (Velocity.X > 0 && value.X > Camera.RightClamp + ShottingGame.k_Width / 2) return;
            base.Position = value;
        }
    }



    private Point _input;
    private HigherState _state = HigherState.Idle;

    public override Point BulletPoint => Position + new Point(3, 6).PointConverter(Direction);



    public Player(Scene scene, Point point) : base(scene, point)
    {
        Type = EntityType.Player;
        Mask = EntityType.Ground | EntityType.Platform | EntityType.Bullet | EntityType.Trigger;

        Width = 5;
        Height = 11;

        _useGravity = true;
        _canMove = true;

        mainWeapon = new Handgun(Scene, this);
        subWeapon = new Granade(Scene, this);

        mainWeapon.Owner = this;
        subWeapon.Owner = this;

        Scene.AddGameObject(mainWeapon);
        Scene.AddGameObject(subWeapon);

        Health = 1;

        _currentPixels = _idlePixels;
        _useGravity = true;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Camera.Position + (20, 5), mainWeapon.Name);
        buffer.WriteText(Camera.Position + (20, 4), mainWeapon.Arms.ToString());
        buffer.WriteText(Camera.Position + (30, 5), subWeapon.Name);
        buffer.WriteText(Camera.Position + (30, 4), subWeapon.Arms.ToString());

        buffer.WriteText(Camera.Position + (0, 79), _isLand.ToString());
        buffer.WriteText(Camera.Position + (0, 78), Aim.ToString());
        buffer.WriteText(Camera.Position + (0, 77), Direction.ToString());
        buffer.WriteText(Camera.Position + (0, 76), _input.ToString());
        buffer.WriteText(Camera.Position + (0, 75), Velocity.ToString());
        //buffer.WriteText(Camera.Position + (0, 74), Velocity.ToString());
        buffer.WriteText(Camera.Position + (0, 73), $"Player : {Position.ToString()}");
        buffer.WriteText(Camera.Position + (0, 72), $"Camera : {Camera.Position.ToString()}");
        buffer.WriteText(Camera.Position + (0, 71), $"LeftClamp : {Camera.LeftClamp.ToString()}");

    }

    public override void Update(float deltaTime)
    {
        PlayerInput();
        Velocity = (_input.X * _speed, Velocity.Y);
        if (_input.X != 0) Direction = (_input.X, 0);

        base.Update(deltaTime);
        
        Aimming();
        CheckMainArms();
        _recoil -= deltaTime;
    }

    

    

    public void Jump()
    {
        if (_isLand)
        {
            _isLand = false;
            Velocity = (Velocity.X, 100);
        }
    }


    


    public void Aimming()
    {
        if (_input.X == 0 && _input.Y == 0)
        {
            Aim = (Direction.X, 0);
        }
        else if (_isLand && _input.Y == -1)
        {
            Aim = (Direction.X, 0);
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

        if (Input.IsKeyDown(ConsoleKey.LeftArrow) && _recoil <= 0)
        {
            _recoil = mainWeapon.Fire(Aim);
        }
        if (Input.IsKeyDown(ConsoleKey.RightArrow) && _recoil <= 0)
        {
            int granadeAim = Velocity.X < 0.1f && Velocity.X > -0.1f ? 1 : 3;
            granadeAim *= Direction.X > 0 ? 1 : -1;

            _recoil = subWeapon.Fire((granadeAim, 3));
        }
        if (Input.IsKeyDown(ConsoleKey.Spacebar))
        {
            Jump();
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