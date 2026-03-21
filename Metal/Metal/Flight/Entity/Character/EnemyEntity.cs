using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class EnemyEntity : CharacterEntity
{
    protected BulletEntity _arms;
    protected int _destinationX;
    protected EnemyState _state;
    
    public EnemyEntity(Scene scene, Point point, EnemyState state = EnemyState.Idle) : base(scene, point)
    {

    }

    public enum EnemyState
    {
        Idle,      // 대기: 플레이어를 발견하기 전
        Chase,     // 추적: 플레이어 방향으로 이동
        Attack,    // 공격: 사거리 내에서 사격/공격 수행
        Avoid,     // 회피: 플레이어의 탄환이나 특정 상황에서 도망
        Stun,      // 경직: 피격 시 잠시 행동 불능
        Dead       // 사망: 사망 애니메이션 및 제거 대기
    }
}