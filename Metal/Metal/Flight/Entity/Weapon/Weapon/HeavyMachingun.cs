using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class HeavyMachingun : Weapon
{
    private float _nextBulletCooldown;

    public HeavyMachingun(Scene scene, CharacterEntity id, bool isMain) : base(scene, id, isMain)
    {
        Name = "HeavyMachingun";
        Arms = 200;
        Cooldown = 0.4f;
    }

    public override void Update(float deltaTime)
    {
        if (_leftCooldown <= 0 && Input.IsKeyDown(_key) && Arms > 0)
        {
            _leftCooldown = Cooldown;
        }

        _leftCooldown -= deltaTime;
    }

    protected override AttackEntity GetArms()
    {
        Arms--;
        return new ShotgunBullet(Scene, _ownerID, _ownerID.BulletPoint, _ownerID.Aim);
    }
}