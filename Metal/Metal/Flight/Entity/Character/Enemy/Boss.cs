using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Boss : EnemyEntity
{
    private float _moveSpeed = 20;
    Point _dir = (-1, 0);

    Cannon _addBazooka_1;
    Cannon _addBazooka_2;
    Lazor _addLazor_1;
    Lazor _addLazor_2;

    Point _AB1_BP { get { return Position + (24, 24); } }
    Point _AB2_BP { get { return Position + (72, 24); } }
    Point _AL1_BP { get { return (12, -40); } }
    Point _AL2_BP { get { return (68, -40); } }

    float _AB1_CD;
    float _AB2_CD;
    float _AL_CD;




    public float LeftAttackCooldown { get; private set; }
    public float MaxAttackCooldown { get; private set; }

    public float LeftMoveTime { get; private set; }
    public float MaxMoveInterval { get; private set; }

    public float LeftDeadDuration { get; private set; }
    public float MaxDeadDuration { get; private set; }

    public int DetectRange { get; private set; }

    

    public Boss(GameScene scene, Point point) : base(scene, point, null, EnemyState.Attack)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet;

        Direction = (1, 0);
        Width = 90;
        Height = 24;
        _canMove = false;
        _useGravity = false;
        Health = 6000;

        rand = new Random();
        _addBazooka_1 = new Cannon(Scene, this);
        _addBazooka_2 = new Cannon(Scene, this);
        _addLazor_1 = new Lazor(Scene, this);
        _addLazor_2 = new Lazor(Scene, this);
        Scene.AddGameObject(_addBazooka_1);
        Scene.AddGameObject(_addBazooka_2);
        Scene.AddGameObject(_addLazor_1);
        Scene.AddGameObject(_addLazor_2);

        MaxMoveInterval = 1f;
        MaxDeadDuration = 5f;

        _currentPixels = _combatPixels;
    }


    public override void Update(float deltaTime)
    {
        if (Health <= 0)
        {
            LeftDeadDuration += deltaTime;
            _currentPixels = _deadPixels;
            if (LeftDeadDuration > MaxDeadDuration)
            {
                if (Scene is StageScene s) s.Ending();
            }
            return;
        }
        if (Scene.player == null) return;

        if (Health < 2500)
        {
            _moveSpeed = 32;
        }

        DoAttack(deltaTime);
        if (LeftMoveTime <= 0) DoMove(deltaTime);
        LeftMoveTime -= deltaTime;
    }
    public override void CollisionFromDynamic(int id, int damage)
    {
        TakeDamage(id, damage);
    }



    public void DoAttack(float deltaTime)
    {
        if (_AB1_CD <= 0)
        {
            _addBazooka_1.Fire(Scene.player.Position - _AB1_BP, _AB1_BP);
            _AB1_CD = rand.Next(4) + 2;
        }
        if (_AB2_CD <= 0)
        {
            _addBazooka_2.Fire(Scene.player.Position - _AB2_BP, _AB2_BP);
            _AB2_CD = rand.Next(4) + 2;
        }
        if (_AL_CD <= 0)
        {
            _addLazor_1.Fire(_AL1_BP, true);
            _addLazor_2.Fire(_AL2_BP, true);
            _AL_CD = rand.Next(4) + 7;
        }

        _AB1_CD -= deltaTime;
        _AB2_CD -= deltaTime;
        _AL_CD -= deltaTime;
    }

    public void DoMove(float deltaTime)
    {
        Position += _dir * _moveSpeed * deltaTime;

        if (Position.X < Camera.LeftClamp) _dir = (1, 0);
        else if (Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 - Width) _dir = (-1, 0);
    }

   

    protected new string[] _combatPixels =
    {
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
    };


    protected new string[] _deadPixels =
    {
        "B   B  BBB  B   B    B B B BBB B   B ",
        " B B  B   B B   B    B B B  B  BB  B ",
        "  B   B   B B   B    B B B  B  B B B ",
        "  B   B   B B   B    B B B  B  B  BB ",
        "  B    BBB   BBB      B B  BBB B   B ",
    };
}