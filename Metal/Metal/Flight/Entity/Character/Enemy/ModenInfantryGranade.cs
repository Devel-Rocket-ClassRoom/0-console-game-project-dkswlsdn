using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class ModenInfantryGrenage : EnemyEntity
{
    public ModenInfantryGrenage(Scene scene, Point point, EnemyState state, Player player) : base(scene, point, state)
    {
        Team = 2;

        RectAngle = new RectAngle(this, (5, 11));

        _arms = new Granade(scene);
        _arms.OwnerID = this;

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
                else if (IsNeerFriendlyDead()) ChangeState(EnemyState.Stun);
                //else if (IsPlayerRebirth()) ChangeState(EnemyState.Stun);
                break;
            case EnemyState.Search:
                if (IsPlayerNeering()) ChangeState(EnemyState.Attack);
                break;
            case EnemyState.Chase:
                if (CanSeePlayer()) ChangeState(EnemyState.Attack);
                else if (IsPlayerNeering()) ChangeState(EnemyState.Attack);
                else if (IsPlayerDead()) ChangeState(EnemyState.Idle);
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Search);
                break;
            case EnemyState.Avoid:
                break;
            case EnemyState.Stun:
                if (IsStunEnd()) ChangeState(EnemyState.Chase);
                break;
            case EnemyState.Dead:
                if (IsEnd())
                {
                    if (Scene is GameScene g)
                    {
                        g.EntityList.Remove(this);
                    }
                    Scene.RemoveGameObject(this); RectAngle = null;
                }
                break;
        }

        if (Health <= 0) ChangeState(EnemyState.Dead);
    }

    public override void DoChase(float deltaTime)
    {
        _currentPixels = _combatPixels;
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
        Aim = (Position.CompareX(ChasingTarget.Position), 2);
        base.DoAttack(deltaTime);
    }

    public override void DoDead(float deltaTime)
    {
        base.DoDead(deltaTime);
        _currentPixels = _deadPixels;
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