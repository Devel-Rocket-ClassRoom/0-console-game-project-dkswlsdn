using System;
using System.Collections.Generic;
using System.Text;


public interface IEnemyAI
{
    void ExecuteStateAction(float deltaTime);
    void CheckTransitions();
    void ChangeState(EnemyState state);
}

public enum EnemyState
{
    Idle,      // 대기
    Gaurd,     // 경계
    Move,     // 추적
    Attack,    // 공격
    Avoid,     // 회피
    Panic,      // 경직
    Dead       // 사망
}