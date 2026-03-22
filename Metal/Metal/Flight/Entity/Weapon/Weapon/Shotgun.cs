using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Shotgun : Weapon
{
    public Shotgun(Scene scene, bool isMain = true) : base(scene, isMain)
    {
        Name = "Shotgun";
        Cooldown = 0.5f;
        _recoil = 0.2f;
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override float Fire(Point dir)
    {
        if (_leftCooldown > 0) return 0f;

        Arms--;
        Scene.AddGameObject(new ShotgunEffect(Scene));
        Scene.AddGameObject(new ShotgunBullet(Scene, Owner.BulletPoint, dir));
        _leftCooldown = Cooldown;
        return _recoil;
    }
}