using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachinegun : Weapon
{
    private float _nextBulletCooldown = 0;
    private int _bulletCount = 4;
    private bool _fire = false;
    private bool _reserve = false;
    private Point _priviousDirection;

    public HeavyMachinegun(Scene scene, CharacterEntity id, bool isMain) : base(scene, id, isMain)
    {
        Name = "HeavyMachingun";
        Arms = 200;
        Cooldown = 0.24f;
    }

    public override void Update(float deltaTime)
    {
        if (Arms <= 0)
            return;

        if (Input.IsKeyDown(_key) && !_reserve)
        {
            if (_leftCooldown <= 0)
            {
                if (_priviousDirection == null)
                    _priviousDirection = _ownerID.Aim;

                _bulletCount = 0;
                _leftCooldown = Cooldown;
            }
            else
            {
                _reserve = true;
            }
        }
        else if (_leftCooldown <= 0 && _reserve)
        {
            _bulletCount = 0;
            _leftCooldown = Cooldown;
            _reserve = false;
        }


        if (_nextBulletCooldown <= 0 && _bulletCount < 4)
        {
            if (Arms <= 0)
                return;

            Arms--;

            Scene.AddGameObject(new HeavyMachinegunBullet(Scene, _ownerID, _ownerID.BulletPoint, _ownerID.Aim, _bulletCount++, _priviousDirection));
            // 앉음시작 이벤트 발생 시 끊음
            _nextBulletCooldown = 0.06f;
        }

        _nextBulletCooldown -= deltaTime;
        _leftCooldown -= deltaTime;
        _priviousDirection = _ownerID.Aim;
    }

    protected override AttackEntity GetArms()
    {
        throw new NotImplementedException();
    }
}