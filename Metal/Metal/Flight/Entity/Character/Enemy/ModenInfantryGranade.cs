using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class ModenInfantryGrenade : EnemyEntity
{
    public ModenInfantryGrenade(Scene scene, Point point, EnemyState state, Player player, int dropRate = 0) : base(scene, point, dropRate, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Width = 5;
        Height = 11;
        _canMove = true;
        _useGravity = true;

        _dropRate = dropRate;

        _arms = new EnemyGranade(scene, this);

        _currentPixels = _combatPixels;
        ChasingTarget = player;

        Health = 1;
        _reconizePlayer = 70;
        _attackBeforeDelay = 0.4f;
        _attackDuration = 3f;
        Direction = (-1, 0);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        //buffer.WriteText(Position, _state.ToString());
    }
    protected override void CheckTransitions()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                if (IsPlayerSuperNeering()) ChangeState(EnemyState.Stun);
                else if (IsNearFriendlyDead()) ChangeState(EnemyState.Stun);
                else if (IsNearFriendlyPanic()) ChangeState(EnemyState.Stun);
                //else if (IsPlayerRebirth()) ChangeState(EnemyState.Stun);
                break;
            case EnemyState.Search:
                if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                break;
            case EnemyState.Chase:
                if (CanSeePlayer()) ChangeState(EnemyState.Attack);
                else if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                else if (IsPlayerDead()) ChangeState(EnemyState.Idle);
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Search);
                break;
            case EnemyState.Avoid:
                break;
            case EnemyState.Stun:
                Mask &= ~EntityType.Bullet;
                if (IsStunEnd()) { ChangeState(EnemyState.Search); Mask |= EntityType.Bullet; }
                break;
            case EnemyState.Dead:
                if (IsEnd())
                {
                    if (rand.Next(100) < _dropRate) Scene.AddGameObject(new GetHeavyMachinegun(Scene, Position));
                    Destroy();
                }
                break;
        }

        if (Health <= 0) { ChangeState(EnemyState.Dead); return; }
        if (IsOutOfCamera()) ChangeState(EnemyState.Chase);
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
        Aim = (Direction.X, 3);
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
        _currentPixels = _combatPixels;
        Position += Direction * 10 * deltaTime;
    }

    public override bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp || Position.X < Camera.LeftClamp;
    }



    //protected new string[] _combatPixels =
    //{
    //    "   ggg   ",
    //    "   gCC   ",
    //    "   gCC   ",
    //    "GGggggGGG",
    //    "GGggggGBG",
    //    "  gBgg   ",
    //    "   ggg   ",
    //    "   g g   ",
    //    "   g g   ",
    //    "   g g   ",
    //    "   B B   ",
    //};
}