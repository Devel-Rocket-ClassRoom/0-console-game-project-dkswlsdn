using System;
using System.Collections.Generic;
using System.Text;


public interface IEnemyAI
{
    void DoIdle(float deltaTime);
    void DoSearch(float deltaTime);
    void DoChase(float deltaTime);
    void DoAttack(float deltaTime);
    void DoAvoid(float deltaTime);
    void DoStun(float deltaTime);
    void DoDead(float deltaTime);

    bool CanSeePlayer();
    bool IsPlayerNearing();
    bool IsPlayerSuperNeering();
    bool IsAttackEnd();
    bool IsPlayerDead();
    bool IsPlayerRebirth();
    bool IsDead();
    bool IsNearFriendlyDead();
    bool IsNearFriendlyPanic();
    bool IsStunEnd();
    bool IsEnd();
    bool IsOutOfCamera();
}

public enum EnemyState
{
    Idle,      // 대기: 플레이어를 발견하기 전
    Search,    // 대기: 플레이어를 발견하기 전
    Chase,     // 추적: 플레이어 방향으로 이동
    Attack,    // 공격: 사거리 내에서 사격/공격 수행
    Avoid,     // 회피: 플레이어의 탄환이나 특정 상황에서 도망
    Stun,      // 경직: 피격 시 잠시 행동 불능
    Dead       // 사망: 사망 애니메이션 및 제거 대기
}