using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Tank : EnemyEntity
{
    public override Point BulletPoint => Position + new Point(29, 16).PointConverter(Direction);

    public Tank(GameScene scene, Point point, EnemyState state, Player player, int dropRate = 0) : base(scene, point, dropRate, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Width = 33;
        Height = 22;
        _canMove = true;

        _dropRate = dropRate;

        _arms = new Cannon(scene);
        _arms.Owner = this;

        _currentPixels = _combatPixels;
        PlayerReferance = player;

        Health = 400;
        _reconizePlayer = 50;
        _attackBeforeDelay = 0.4f;
        _deadDuration = 3;
        _attackDuration = 2.5f;
        Direction = (-1, 0);
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position + (0, 0), _dropRate.ToString());
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
                ChangeState(EnemyState.Chase);
                break;
            case EnemyState.Search:
                if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                else if (!IsPlayerNearing()) ChangeState(EnemyState.Chase);
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
                break;
            case EnemyState.Dead:
                Width = 1;
                Height = 1;
                if (IsEnd())
                {
                    Scene.AddGameObject(new ModenInfantryCannon(Scene, Position, EnemyState.Stun, PlayerReferance, _dropRate));
                    Destroy();
                }
                break;
        }

        if (Health <= 0) ChangeState(EnemyState.Dead);
        else if (IsOutOfCamera()) ChangeState(EnemyState.Chase);
    }

    public override void DoIdle(float deltaTime)
    {
    }

    public override void DoStun(float deltaTime)
    {
    }

    public override void DoSearch(float deltaTime)
    {
        _currentPixels = _combatPixels;
        int n = PlayerReferance.Position.X - Position.X > 0 ? 1 : -1;
        Direction = (n, 0);
    }

    public override void DoChase(float deltaTime)
    {
        _currentPixels = _combatPixels;
        Position += Direction * 10 * deltaTime;
    }

    public override void DoAttack(float deltaTime)
    {
        _currentPixels = _combatPixels;
        Aim = Direction;
        base.DoAttack(deltaTime);
    }

    public override void DoDead(float deltaTime)
    {
        base.DoDead(deltaTime);
        _currentPixels = _deadPixels;
    }

    public override bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 - 20 || Position.X < Camera.LeftClamp;
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