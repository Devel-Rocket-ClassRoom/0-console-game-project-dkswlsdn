using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Tank : EnemyEntity, IEnemyAI, IAttackable, IMoveable, IDeadable, IPlayerDetectable
{
    public override Point BulletPoint => Position + new Point(29, 16).PointConverter(Direction);

    public float LeftAttackCooldown { get; private set; }
    public float MaxAttackCooldown { get; private set; }

    public float LeftDeadDuration { get; private set; }
    public float MaxDeadDuration { get; private set; }

    public int DetectRange { get; private set; }

    public float LeftMoveTime { get; private set; }
    public float MaxMoveInterval { get; private set; }

    public Tank(GameScene scene, Point point, EnemyState state, GetWeapon dropItem) : base(scene, point, dropItem, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Width = 33;
        Height = 22;
        _canMove = true;

        _arms = new Cannon(scene, this);
        _arms.Owner = this;

        Health = 400;
        MaxAttackCooldown = 1f;
        MaxMoveInterval = 0.5f;
        MaxDeadDuration = 3f;
        DetectRange = 50;

        Direction = (-1, 0);

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


    public void CheckTransitions()
    {
        if (Health <= 0)
        {
            if (State != EnemyState.Dead) ChangeState(EnemyState.Dead);
            if (IsDeadEnd()) { DropItem(); Scene.AddGameObject(new ModenInfantryCannon(Scene, Position, EnemyState.Panic, new GetShotgun(Scene, (0, 0)))); Destroy(); }
            return;
        }
        if (Scene.player == null) { ChangeState(EnemyState.Idle); return; }

        switch (State)
        {
            case EnemyState.Move:
                if (IsMoveEnd()) ChangeState(EnemyState.Gaurd);
                break;
            case EnemyState.Gaurd:
                if (IsOutOfCamera()) ChangeState(EnemyState.Move);
                else if (IsPlayerInRange()) ChangeState(EnemyState.Attack);
                else ChangeState(EnemyState.Move);
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Gaurd);
                break;
        }
    }

    public void DoMove(float deltaTime)
    {
        Position += Direction * 10 * deltaTime;
        LeftMoveTime -= deltaTime;
    }

    public void DoAttack(float deltaTime)
    {
        LeftAttackCooldown -= deltaTime;
        if (LeftAttackCooldown > 0) return;

        _arms.Fire(Direction);
        LeftAttackCooldown = MaxAttackCooldown;
    }

    public void DoDead(float deltaTime)
    {
        LeftDeadDuration -= deltaTime;
    }

    public bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 - 40 || Position.X < Camera.LeftClamp;
    }





    public void ChangeState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Move:
                _currentPixels = _combatPixels;
                LeftMoveTime = MaxMoveInterval;
                break;
            case EnemyState.Gaurd:
                _currentPixels = _combatPixels;
                break;
            case EnemyState.Attack:
                _currentPixels = _combatPixels;
                LeftAttackCooldown = MaxAttackCooldown;
                break;
            case EnemyState.Dead:
                LeftDeadDuration = MaxDeadDuration;
                _currentPixels = _deadPixels;
                break;
        }

        State = state;
    }

    public void ExecuteStateAction(float deltaTime)
    {
        switch (State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Move:
                DoMove(deltaTime);
                break;
            case EnemyState.Attack:
                DoAttack(deltaTime);
                break;
            case EnemyState.Dead:
                DoDead(deltaTime);
                break;
        }
    }

    public bool IsAttackEnd()
    {
        return LeftAttackCooldown <= 0;
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
    public void ChangeDirection()
    {
        if (Scene.player == null) return;

        Direction = (Position.CompareX(Scene.player.Position), 0);
    }

    public bool IsDeadEnd()
    {
        return LeftDeadDuration <= 0;
    }

    public bool IsPlayerSuperNearing()
    {
        throw new NotImplementedException();
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

    protected new string[] _combatPixels =
    {
        "            GGGGGGGG             ",
        "         yyyyyyyyyyyyyy          ",
        "        yyyyyyyyyyyyyyyyyyyyy    ",
        "        yyyyyyyyyyyyyyyyyyyyy    ",
        "        BBBBBBBBBBBBBBBB         ",
        " BBBBBBByyyyyyyyyyyyyyyyyyyyy    ",
        " yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy ",
        "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        " yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        "      GGGGGyyyyyyyyyyyyyyyyyyyyyy",
        "    GG               yyyyyyyyyyy ",
        "   G   GGG             GGG   G   ",
        "  G   GBBBG           GBBBG   G  ",
        "  G   GBGBG           GBGBG   G  ",
        "  G   GBBBG           GBBBG   G  ",
        "   G   GGG             GGG   G   ",
        "    GG                     GG    ",
        "      GGGGGGGGGGGGGGGGGGGGG      ",
    };

    protected new string[] _deadPixels =
    {
        "  R R       GGGGGGGG             ",
        "   R     yyyyyyyyyyyyyy        R ",
        "  R R   yyyyyyyyyyyyyyyyyyyyy RRR",
        "        yyyyyyyyyyyyyyyyyyyyy  R ",
        "        BBBBBBBBBBBBBBBB         ",
        " BBBBBBByyyyyyyyyyyyyyyyyyyyy    ",
        " yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy ",
        "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        " yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
        "      GGGGGyyyyyyyyyyyyyyyyyyyyyy",
        "    GG               yyyyyyyyyyy ",
        "   G   GGG   R R       GGG   G   ",
        "  G   GBBBG   R       GBBBG   G  ",
        "  G   GBGBG  R R      GBGBG   G  ",
        "  G   GBBBG           GBBBG   G  ",
        "   G   GGG             GGG   G   ",
        "    GG                     GG    ",
        "      GGGGGGGGGGGGGGGGGGGGG      ",
    };
}