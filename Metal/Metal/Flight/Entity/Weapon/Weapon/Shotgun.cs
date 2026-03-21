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
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    protected override void Fire()
    {
        Arms--;
        new ShotgunBullet(Scene, OwnerID, OwnerID.BulletPoint, OwnerID.Aim);
    }
}