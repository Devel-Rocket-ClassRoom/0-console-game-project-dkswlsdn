using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Shotgun : Weapon
{
    public Shotgun(Scene scene, CharacterEntity id, bool isMain = true) : base(scene, id, isMain)
    {
        Name = "Shotgun";
        Cooldown = 0.5f;
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    protected override AttackEntity GetArms()
    {
        Arms--;
        return new ShotgunBullet(Scene, _ownerID, _ownerID.BulletPoint, _ownerID.Aim);
    }
}