using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using Framework.Engine;


public class ModenInfantryGranade : EnemyEntity, IEnemyAI, IAttackable, IMoveable, IGuardable, IFriendlyDetectable, IPlayerDetectable, IStunable, IDeadable
{
    public override Point BulletPoint => Position + new Point(3, 6).PointConverter(Direction);

    public float LeftAttackCooldown { get; private set; }
    public float MaxAttackCooldown { get; private set; }

    public float LeftStunDuration { get; private set; }
    public float MaxStunDuration { get; private set; }

    public float LeftDeadDuration { get; private set; }
    public float MaxDeadDuration { get; private set; }

    public int DetectRange { get; private set; }

    public float LeftMoveTime { get; private set; }
    public float MaxMoveInterval { get; private set; }


    public ModenInfantryGranade(GameScene scene, Point point, EnemyState state, GetWeapon dropItem, int direction = -1) : base(scene, point, dropItem, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Direction = (direction, 0);
        Width = 5;
        Height = 11;
        _canMove = true;
        _useGravity = true;

        _arms = new EnemyGranade(scene, this);
        Health = 100;



        MaxAttackCooldown = 3f;
        MaxMoveInterval = 0.5f;
        MaxStunDuration = 0.5f;
        MaxDeadDuration = 0.5f;

        DetectRange = 50;
        ChangeState(state);
    }


    public override void Update(float deltaTime)
    {
        CheckTransitions();
        ExecuteStateAction(deltaTime);

        base.Update(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }

    public override void CollisionFromDynamic(int id, int damage)
    {
        TakeDamage(id, damage);
    }



    public void ExecuteStateAction(float deltaTime)
    {
        switch (State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Move:
                ChangeDirection();
                DoMove(deltaTime);
                break;
            case EnemyState.Gaurd:
                DoGaurd(deltaTime);
                break;
            case EnemyState.Attack:
                DoAttack(deltaTime);
                break;
            case EnemyState.Panic:
                DoStun(deltaTime);
                break;
            case EnemyState.Dead:
                DoDead(deltaTime);
                break;
        }
    }

    public void CheckTransitions()
    {
        if (Health <= 0)
        {
            if (State != EnemyState.Dead) ChangeState(EnemyState.Dead);
            if (IsDeadEnd()) { DropItem(); Destroy(); }
            return;
        }
        else if (Scene.player == null) { ChangeState(EnemyState.Idle); return; }

        switch (State)
        {
            case EnemyState.Idle:
                if (IsPlayerSuperNearing() || IsNearFriendlyDead() || IsNearFriendlyPanic()) ChangeState(EnemyState.Panic);
                break;
            case EnemyState.Move:
                if (!IsPlayerInRange() || IsOutOfCamera()) ChangeState(EnemyState.Move);
                else if (IsMoveEnd()) ChangeState(EnemyState.Gaurd);
                break;
            case EnemyState.Gaurd:
                if (IsOutOfCamera()) ChangeState(EnemyState.Move);
                else if (IsPlayerInRange()) ChangeState(EnemyState.Attack);
                else ChangeState(EnemyState.Move);
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Gaurd);
                break;
            case EnemyState.Panic:
                if (IsStunEnd()) ChangeState(EnemyState.Gaurd);
                break;
        }
    }

    public void DoAttack(float deltaTime)
    {
        LeftAttackCooldown -= deltaTime;
        if (LeftAttackCooldown > 0) return;

        _arms.Fire(Aim);
        LeftAttackCooldown = MaxAttackCooldown;
    }

    public bool IsAttackEnd()
    {
        return LeftAttackCooldown <= 0;
    }

    public void DoGaurd(float deltaTime) { }

    public bool IsNearFriendlyDead()
    {
        for (int i = 0; i < Scene.DynamicEntityList.Count; i++)
        {
            if (Scene.DynamicEntityList[i] is EnemyEntity e && Position.CompareX(e.Position) < 20 && e.State == EnemyState.Dead)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsNearFriendlyPanic()
    {
        for (int i = 0; i < Scene.DynamicEntityList.Count; i++)
        {
            if (Scene.DynamicEntityList[i] is EnemyEntity e && Position.CompareX(e.Position) < 20 && e.State == EnemyState.Panic)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsPlayerSuperNearing()
    {
        return Position.IsInDistance(Scene.player.Position, 20);
    }

    public bool IsPlayerDead()
    {
        return !Scene.player.IsAlive;
    }

    public bool IsPlayerRebirth()
    {
        throw new NotImplementedException();
    }

    public bool IsPlayerInRange()
    {
        return Position.IsInDistance(Scene.player.Position, DetectRange);
    }

    public void DoStun(float deltaTime)
    {
        LeftStunDuration -= deltaTime;
    }

    public bool IsStunEnd()
    {
        return LeftStunDuration <= 0;
    }

    public void DoDead(float deltaTime)
    {
        LeftDeadDuration -= deltaTime;
    }

    public bool IsDeadEnd()
    {
        return LeftDeadDuration <= 0;
    }

    public void DoMove(float deltaTime)
    {
        Position += Direction * 10 * deltaTime;
        LeftMoveTime -= deltaTime;
    }
    public void ChangeDirection()
    {
        if (Scene.player == null) return;

        Direction = (Position.CompareX(Scene.player.Position), 0);
    }

    public bool IsMoveEnd()
    {
        if (LeftMoveTime <= 0)
        {
            ChangeDirection();
            return true;
        }

        return false;
    }

    public bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 - 20 || Position.X < Camera.LeftClamp;
    }

    public void ChangeState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                _currentPixels = _idlePixels;
                break;
            case EnemyState.Move:
                _currentPixels = _idlePixels;
                LeftMoveTime = MaxMoveInterval;
                break;
            case EnemyState.Gaurd:
                _currentPixels = _combatPixels;
                break;
            case EnemyState.Attack:
                _currentPixels = _combatPixels;
                break;
            case EnemyState.Panic:
                _currentPixels = _PanicPixels;
                LeftStunDuration = MaxStunDuration;
                break;
            case EnemyState.Dead:
                LeftDeadDuration = MaxDeadDuration;
                _currentPixels = _deadPixels;
                break;
        }
        
        State = state;
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