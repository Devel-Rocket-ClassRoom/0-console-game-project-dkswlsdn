using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Boss : EnemyEntity
{
    public override Point BulletPoint => Position + new Point(3, 6).PointConverter(Direction);
    Point _destination;

    public Boss(Scene scene, Point point, EnemyState state, Player player, int direction = -1, int dropRate = 0) : base(scene, point, dropRate, state)
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
        ChasingTarget = player;

        Health = 5000;
        _reconizePlayer = 1000;
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
                break;
            case EnemyState.Search:
                if (IsPlayerNearing()) ChangeState(EnemyState.Attack);
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                if (IsAttackEnd()) ChangeState(EnemyState.Search);
                break;
            case EnemyState.Avoid:
                break;
            case EnemyState.Stun:
                break;
            case EnemyState.Dead:
                if (IsEnd())
                {
                    GameScene.IsPlayerWin = true;
                    Destroy();
                }
                break;
        }

        if (Health <= 0) ChangeState(EnemyState.Dead);
    }

    public override void DoSearch(float deltaTime)
    {
        _currentPixels = _combatPixels;
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

    public override void DoChase(float deltaTime)
    {
        base.DoChase(deltaTime);

        Position += Direction * 10 * deltaTime;
    }

    public override bool IsOutOfCamera()
    {
        return Position.X > Camera.RightClamp + ShottingGame.k_Width / 2 || Position.X < Camera.LeftClamp;
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