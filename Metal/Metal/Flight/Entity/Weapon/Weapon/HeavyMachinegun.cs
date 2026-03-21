using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegun : Weapon
{
    private float _nextBulletCooldown = 0;
    private int _bulletCount = 0;
    private int _spreadingBulletCount = 0;
    private int _bulletPatternCount = 0;
    private bool _reserve = false;
    private bool _isSpreading;
    private Point _previousDirection = (0, 0);
    private Point _bulletDirection;
    private Point _nowAim;
    Point finalDir;


    public HeavyMachinegun(Scene scene, bool isMain = true) : base(scene, isMain)
    {
        Name = "HeavyMachingun";
        Cooldown = 0.30f;
    }



    public override void Update(float deltaTime)
    {
        if (Arms <= 0) return;

        HandleInput();
        HandleBurstFire();
        UpdateTimers(deltaTime);
    }

    private void HandleInput()
    {
        if (Input.IsKeyDown(_key))
        {
            _bulletCount = 4;
        }
    }

    private void HandleBurstFire()
    {
        if (_nextBulletCooldown <= 0 && _bulletCount > 0)
        {
            if (_previousDirection.X == 0 && _previousDirection.Y == 0)
                _previousDirection = OwnerID.Aim;

            // 2. 방향 결정 및 모드 체크
            

            if (_spreadingBulletCount <= 0)
            {
                if (Is90DegreeTurn(_previousDirection, OwnerID.Aim))
                {
                    _spreadingBulletCount = 3; // 흩뿌리기 4발 할당
                    finalDir = OwnerID.Aim;     // 꺾인 시점의 방향 고정
                }
                else
                {
                    finalDir = OwnerID.Aim;     // 일반 사격은 실시간 조준
                    _previousDirection = OwnerID.Aim; // 일반 사격 시에만 기준점 추적
                }
            }
            else
            {
                _spreadingBulletCount--;
            }

            // 3. 발사 실행
            Fire(finalDir);
            _bulletCount--;

            // 4. 흩뿌리기가 방금 막 끝났다면, 그 시점의 에임을 다음 기준점으로 설정
            if (_spreadingBulletCount == 0)
            {
                _previousDirection = finalDir;
            }
        }
    }

    public override void Fire(Point finalDir) // 매개변수로 방향을 받도록 수정
    {
        Arms--;

        if (_spreadingBulletCount == 3 || _bulletPatternCount == 4) _bulletPatternCount = 0;
        
        new HeavyMachinegunBullet(Scene, OwnerID, OwnerID.BulletPoint, finalDir, _bulletPatternCount++, _previousDirection);
        _nextBulletCooldown = 0.06f;
    }

    private void UpdateTimers(float deltaTime)
    {
        _nextBulletCooldown -= deltaTime;
        _leftCooldown -= deltaTime;

        // 모든 사격 시퀀스가 완전히 종료되면 방향 초기화
        if (_nextBulletCooldown <= -0.2f && _leftCooldown <= -0.2f)
        {
            _previousDirection = (0, 0);
            _spreadingBulletCount = 0;
            _bulletCount = 0;
        }
    }

    private bool Is90DegreeTurn(Point prev, Point current)
    {
        if (prev.X == 0 && prev.Y == 0) return false;

        return (prev.X != current.X && prev.Y != current.Y);
    }
}