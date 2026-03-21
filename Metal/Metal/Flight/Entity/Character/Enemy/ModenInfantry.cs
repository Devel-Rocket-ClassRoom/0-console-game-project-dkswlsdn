using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ModenInfantryCannon : EnemyEntity
{
    public ModenInfantryCannon(Scene scene, Point point, EnemyState state, Player player) : base(scene, point, state)
    {
        RectAngle = new RectAngle(this, (5, 11));
        
        _arms = new Cannon(scene);
        _arms.OwnerID = this;

        _currentPixels = _combatPixels;
        ChasingTarget = player;

        Health = 100;
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
                if (IsPlayerNeering()) ChangeState(EnemyState.Stun);
                //else if (IsNeerFriendlyDead()) ChangeState(EnemyState.Stun);
                else if (IsPlayerRebirth()) ChangeState(EnemyState.Stun);
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
                if (IsEnd()) Scene.RemoveGameObject(this);
                break;
        }

        if (Health <= 0) ChangeState(EnemyState.Dead);
    }

    public override void DoSearch(float deltaTime)
    {
        int n = ChasingTarget.Position.X - Position.X > 0 ? 1 : -1;
        Direction = (n, 0);
    }

    public override void DoAttack(float deltaTime)
    {
        Aim = (ChasingTarget.Position - Position).HexaNormalize();
        base.DoAttack(deltaTime);
    }

    public override void DoDead(float deltaTime)
    {
        base.DoDead(deltaTime);
        _currentPixels = _deadPixels;
    }

    private string[] _idlePixels =
    {
        " ggg ",
        " gCC ",
        " gCC ",
        "ggggg",
        "ggggg",
        "ggggg",
        "BgggB",
        " g g ",
        " g g ",
        " g g ",
        " B B ",
    };

    private string[] _combatPixels =
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

    private string[] _deadPixels =
    {
        "           ",
        "           ",
        "           ",
        "           ",
        "           ",
        "           ",
        "   gggB    ",
        "gCCgggggggB",
        "gCCgggg    ",
        "ggggggggggB",
        "   gggB    ",
    };
}

//public class ModenInfantryShiled : EnemyEntity, IMoveable, IJumpable
//{


//    public ModenInfantryShiled(Scene scene, Point point) : base(scene, point)
//    {
//    }
//}

//public class ModenInfantryCannon : EnemyEntity, IMoveable, IJumpable
//{
//    public ModenInfantryCannon(Scene scene, Point point) : base(scene, point)
//    {
//    }
//}