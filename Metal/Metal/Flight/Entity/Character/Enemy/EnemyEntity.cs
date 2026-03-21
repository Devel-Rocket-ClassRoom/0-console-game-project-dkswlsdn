using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class EnemyEntity : CharacterEntity, IEnemyAI
{
    protected float _attackTimer = 0f;
    protected const float k_AttackDuration = 1.2f;

    protected float _stunTimer = 0f;
    protected const float k_stunDuration = 1.2f;

    protected float _deadTimer = 0f;
    protected const float k_deadDuration = 1.0f;

    public Player ChasingTarget;

    protected Weapon _arms;

    
    protected EnemyState _state;


    public Point Aim { get; protected set; }



    public EnemyEntity(Scene scene, Point point, EnemyState state = EnemyState.Idle) : base(scene, point)
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
                if (IsEnd()) Scene.RemoveGameObject(this);
                break;
                
        }
    }
    protected void ChangeState(EnemyState state) { _state = state; }

    public virtual void DoAttack(float deltaTime)
    {
        if (_attackTimer == 0f)
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

    public virtual bool IsPlayerNeering()
    {
        return Position.IsInDistance(ChasingTarget.Position, 100f);
    }

    public virtual bool IsAttackEnd()
    {
        if (_attackTimer >= k_AttackDuration)
        {
            _attackTimer = 0;
            return true;
        }

        return false;
    }

    public virtual bool IsPlayerDead()
    {
        return !ChasingTarget.IsAlive;
    }

    public virtual bool IsPlayerRebirth()
    {
        return ChasingTarget.IsAlive;
    }

    public virtual bool IsDead()
    {
        return !IsAlive;
    }

    public virtual bool IsNeerFriendlyDead()
    {
        throw new NotImplementedException();
    }

    public virtual bool IsStunEnd()
    {
        if (_stunTimer >= k_stunDuration)
        {
            _stunTimer = 0;
            return true;
        }

        return false;
    }

    public bool IsEnd()
    {
        return _deadTimer >= k_deadDuration;
    }
}