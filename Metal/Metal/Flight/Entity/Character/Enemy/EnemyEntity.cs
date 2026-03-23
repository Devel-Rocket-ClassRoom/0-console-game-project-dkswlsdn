using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class EnemyEntity : CharacterEntity, IEnemyAI
{
    protected Random rand = new Random();
    protected int _dropRate;

    protected float _attackTimer = 0f;
    protected float _attackDuration = 1.2f;
    protected float _attackBeforeDelay;

    protected float _stunTimer = 0f;
    protected float _stunDuration = 0.5f;

    protected float _deadTimer = 0f;
    protected float _deadDuration = 0.3f;

   
    protected float _reconizePlayer;

    protected Weapon _arms;

    
    protected EnemyState _state;


    public Point Aim { get; protected set; }



    public EnemyEntity(GameScene scene, Point point, int dropRate, EnemyState state = EnemyState.Idle) : base(scene, point)
    {
        _state = state;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        ExecuteStateAction(deltaTime);
        CheckTransitions();
    }

    private void ExecuteStateAction(float deltaTime)
    {
        if (PlayerReferance == null) _state = EnemyState.Idle;

        switch (_state)
        {
            case EnemyState.Idle: DoIdle(deltaTime); break;
            case EnemyState.Search: DoSearch(deltaTime); break;
            case EnemyState.Chase: DoChase(deltaTime); break;
            case EnemyState.Attack: DoAttack(deltaTime); break;
            case EnemyState.Avoid: DoAvoid(deltaTime); break;
            case EnemyState.Stun: DoStun(deltaTime); break;
            case EnemyState.Dead: DoDead(deltaTime); break;
        }
    }
    protected virtual void CheckTransitions()
    {


        switch (_state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Search:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Avoid:
                break;
            case EnemyState.Stun:
                break;
            case EnemyState.Dead:
                if (IsEnd()) { Destroy(); }
                break;
                
        }

        if (!IsAlive) ChangeState(EnemyState.Dead);
        else if (!PlayerReferance.IsAlive) PlayerReferance = null;
    }
    protected void ChangeState(EnemyState state) { _state = state; }

    public virtual void DoAttack(float deltaTime)
    {
        if (_attackTimer == 0)
        {
            _arms.Fire(Aim);
        }

        _attackTimer += deltaTime;
    }

    public virtual void DoAvoid(float deltaTime)
    {
        throw new NotImplementedException();
    }

    public virtual void DoChase(float deltaTime)
    {
        
    }

    public virtual void DoDead(float deltaTime)
    {
        _deadTimer += deltaTime;
    }

    public virtual void DoIdle(float deltaTime)
    {
        
    }
    public virtual void DoSearch(float deltaTime)
    {
        
    }

    public virtual void DoStun(float deltaTime)
    {
        _stunTimer += deltaTime;
    }

    public virtual bool CanSeePlayer()
    {
        return false;
    }

    public virtual bool IsPlayerNearing()
    {
        return Position.IsInDistance(PlayerReferance.Position, _reconizePlayer);
    }

    public virtual bool IsAttackEnd()
    {
        if (_attackTimer >= _attackDuration)
        {
            _attackTimer = 0;
            return true;
        }

        return false;
    }

    public virtual bool IsPlayerDead()
    {
        return !PlayerReferance.IsAlive;
    }

    public virtual bool IsPlayerRebirth()
    {
        return false;
    }

    public virtual bool IsDead()
    {
        return !(_state == EnemyState.Dead); ;
    }

    public virtual bool IsNearFriendlyDead()
    {
        for (int i = 0; i < Scene.DynamicEntityList.Count; i++)
        {
            Entity e = Scene.DynamicEntityList[i];
            if (e == this) continue; 

            if ((e.Type & EntityType.Enemy) != 0)
            {
                if (Position.IsInDistance(e.Position, 30f))
                {
                    if (e is EnemyEntity enemy && enemy._state == EnemyState.Dead)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public virtual bool IsStunEnd()
    {
        if (_stunTimer >= _stunDuration)
        {
            _stunTimer = 0;
            return true;
        }

        return false;
    }

    public virtual bool IsEnd()
    {
        return _deadTimer >= _deadDuration;
    }

    public virtual bool IsPlayerSuperNeering()
    {
        return Position.IsInDistance(PlayerReferance.Position, 20f);
    }

    public bool IsNearFriendlyPanic()
    {
        for (int i = 0; i < Scene.DynamicEntityList.Count; i++)
        {
            Entity e = Scene.DynamicEntityList[i];
            if (e == this) continue;

            if ((e.Type & EntityType.Enemy) != 0)
            {
                if (Position.IsInDistance(e.Position, 30f))
                {
                    if (e is EnemyEntity enemy && enemy._state == EnemyState.Stun)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public abstract bool IsOutOfCamera();

    protected string[] _idlePixels =
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

    protected string[] _stunPixels =
    {
        "B ggg B",
        "g gCC g",
        "g gCC g",
        " ggggg ",
        "  ggg  ",
        "  ggg  ",
        "  ggg  ",
        "  g g  ",
        " g   g ",
        " g   g ",
        "B     B",
    };

    protected string[] _combatPixels =
    {
        "  ggg  ",
        "  gCC  ",
        "  gCC  ",
        " ggggg ",
        "g ggg g",
        "g ggg g",
        "B ggg B",
        "  g g  ",
        "  g g  ",
        "  g g  ",
        "  B B  ",
    };

    protected string[] _deadPixels =
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