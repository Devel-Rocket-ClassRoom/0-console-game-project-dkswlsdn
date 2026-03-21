using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Handgun : Weapon
{
    public Handgun(Scene scene, CharacterEntity id, bool isMain = true) : base(scene, isMain)
    {
        Name = "Handgun";
        Arms = 1000;
        Cooldown = 0.1f;
        OwnerID = id;
    }

    protected override void Fire()
    {
        new HandgunBullet(Scene, OwnerID, OwnerID.BulletPoint, OwnerID.Aim);
    }
}