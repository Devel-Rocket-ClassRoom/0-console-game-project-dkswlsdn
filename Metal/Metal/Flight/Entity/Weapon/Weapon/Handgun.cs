using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Handgun : Weapon
{
    public Handgun(Scene scene, CharacterEntity id, bool isMain = true) : base(scene, id, isMain)
    {
        Name = "Handgun";
        Arms = 1000;
        Cooldown = 0.1f;
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    protected override AttackEntity GetArms()
    {
        return new HandgunBullet(Scene, _ownerID, _ownerID.BulletPoint, _ownerID.Aim);
    }
}