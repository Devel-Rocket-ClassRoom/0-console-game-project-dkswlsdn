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
    private bool _canShot = true;

    private bool _useImmune = false;


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
    //public PlayerState State { get; private set; }
    private float _deadDuration = 1f;

    public override Point BulletPoint => Position + new Point(3, 6).PointConverter(Direction);



    public Player(GameScene scene, Point point) : base(scene, point)
    {
        Type = EntityType.Player;
        Mask = EntityType.Ground | EntityType.Platform | EntityType.Trigger;

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
    }

    public override void Update(float deltaTime)
    {
        PlayerInput(deltaTime);
        CheatKey();

        Velocity = (_input.X * _speed, Velocity.Y);
        if (_input.X != 0) Direction = (_input.X, 0);

        base.Update(deltaTime);
        
        Aimming();
        CheckMainArms();
        _recoil -= deltaTime;

        if (_isLand && !_useImmune) Mask |= EntityType.Bullet;
    }


    public override void CollisionFromDynamic(int id, int damage)
    {
        TakeDamage(id, damage);
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
    private void PlayerInput(float deltaTime)
    {
        if (!IsAlive)
        {
            _currentPixels = _deadPixels;

            _deadDuration -= deltaTime;

            if (_deadDuration <= 0)
            {
                Destroy();
            }

            return;
        }

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
            float n = _isLand && _input.Y == -1 ? 0.8f : 1;
            _recoil = mainWeapon.Fire(Aim) * n;
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

    private void CheatKey()
    {
        if (!Input.IsKey(ConsoleKey.N)) return;

        if (Input.IsKeyDown(ConsoleKey.D1)) { mainWeapon = new HeavyMachinegun(Scene); Scene.AddGameObject(mainWeapon); mainWeapon.Owner = this; }
        if (Input.IsKeyDown(ConsoleKey.D2)) { mainWeapon = new Shotgun(Scene); Scene.AddGameObject(mainWeapon); mainWeapon.Owner = this; }
        if (Input.IsKeyDown(ConsoleKey.D3)) mainWeapon.Arms = 10000;
        if (Input.IsKeyDown(ConsoleKey.D4)) subWeapon.Arms = 10000;
        if (Input.IsKeyDown(ConsoleKey.D5)) { _useImmune = true; Mask &= ~EntityType.Bullet; }
        if (Input.IsKeyDown(ConsoleKey.D6)) { if (Scene is StageScene s) s.Ending(); }
    }
    public void Jump()
    {
        if (_isLand)
        {
            _isLand = false;
            Velocity = (Velocity.X, 100);
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
    



    //private void ExecuteStateAction(float deltaTime)
    //{
    //    switch (State)
    //    {
    //        case PlayerState.Idle: DoIdle(deltaTime); break;
    //        case PlayerState.SitDown: DoIdle(deltaTime); break;
    //        case PlayerState.Jump: DoIdle(deltaTime); break;
    //        case PlayerState.Shot: DoIdle(deltaTime); break;
    //        case PlayerState.Dead: DoIdle(deltaTime); break;
    //    }
    //}

    //protected void ChangeState(PlayerState state) { State = state; }

    //void DoIdle(float deltaTime)
    //{
    //    _currentPixels = _idlePixels;
    //}

    //void DoSitDown(float deltaTime)
    //{
    //    _currentPixels = _sitPixels;
    //}
    //void DoJump(float deltaTime)
    //{
    //    _currentPixels = _idlePixels;
    //}

    //void DoDoShotIdle(float deltaTime)
    //{
    //    _currentPixels = _idlePixels;
    //}
    //void DoDead(float deltaTime)
    //{
    //    _currentPixels = _de;
    //}

    //bool IsEnd()
    //{

    //}



    public enum PlayerState
    {
        Idle,
        Shot,
        Jump,
        SitDown,
        Dead
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

    protected string[] _deadPixels =
    {
        "   BBBD    ",
        "DCCBGGBBBBD",
        "DCCBGGB    ",
        "DDDBGGBBBBD",
        "   BBBD    ",
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