using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ModenInfantryCannon : EnemyEntity
{
    public override Point BulletPoint => Position + new Point(3, 6).PointConverter(Direction);

    public ModenInfantryCannon(Scene scene, Point point, EnemyState state, Player player, int dropRate, int direction = -1) : base(scene, point, dropRate, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Width = 5;
        Height = 11;
        _canMove = true;
        _useGravity = true;

        _dropRate = dropRate;

        _arms = new Cannon(scene);
        _arms.Owner = this;

        _currentPixels = _combatPixels;
        ChasingTarget = player;

        Health = 1;
        _reconizePlayer = 100;
        _attackBeforeDelay = 0.4f;
        _attackDuration = 1.5f;
        Direction = (direction, 0);
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position + (0, 0), Health.ToString());
    }

    public override void CollisionFromDynamic(int id, int damage)
    {
        TakeDamage(id, damage);
    }


    protected override void CheckTransitions()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                if (IsPlayerSuperNeering()) ChangeState(EnemyState.Stun);
                else if (IsNearFriendlyDead()) ChangeState(EnemyState.Stun);
                else if (IsNearFriendlyPanic()) ChangeState(EnemyState.Stun);
                break;
            case EnemyState.Search:
                if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                break;
            case EnemyState.Chase:
                if (CanSeePlayer()) ChangeState(EnemyState.Attack);
                else if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Search);
                break;
            case EnemyState.Avoid:
                break;
            case EnemyState.Stun:
                if (IsStunEnd()) { ChangeState(EnemyState.Search); }
                break;
            case EnemyState.Dead:
                if (IsEnd())
                {
                    if (_dropRate == 100) Scene.AddGameObject(new GetShotgun(Scene, Position));
                    Destroy();
                }
                break;
        }

        if (Health <= 0) ChangeState(EnemyState.Dead);
        else if (IsOutOfCamera()) ChangeState(EnemyState.Chase);
    }

    public override void DoIdle(float deltaTime)
    {
        _currentPixels = _idlePixels;
    }

    public override void DoStun(float deltaTime)
    {
        base.DoStun(deltaTime);
        _currentPixels = _stunPixels;
    }

    public override void DoSearch(float deltaTime)
    {
        _currentPixels = _combatPixels;
        int n = ChasingTarget.Position.X - Position.X > 0 ? 1 : -1;
        Direction = (n, 0);
    }

    public override void DoAttack(float deltaTime)
    {
        _currentPixels = _combatPixels;
        Aim = (ChasingTarget.Position - Position).HexaNormalize();
        base.DoAttack(deltaTime);
    }

    public override void DoDead(float deltaTime)
    {
        base.DoDead(deltaTime);
        _currentPixels = _deadPixels;
    }

    public override void DoChase(float deltaTime)
    {
        base.DoChase(deltaTime);

        Position += Direction * 10 * deltaTime;
    }

    public override bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 - 20 || Position.X < Camera.LeftClamp;
    }


    public override void DrawEntity(ScreenBuffer buffer)
    {
        base.DrawEntity(buffer);
        buffer.WriteText(Position, _dropRate.ToString());
    }


    protected new string[] _combatPixels =
    {
        "   ggg   ",
        "   gCC   ",
        "   gCC   ",
        "GGggggGGG",
        "GGggggGBG",
        "  gBgg   ",
        "   ggg   ",
        "   g g   ",
        "   g g   ",
        "   g g   ",
        "   B B   ",
    };

    
}