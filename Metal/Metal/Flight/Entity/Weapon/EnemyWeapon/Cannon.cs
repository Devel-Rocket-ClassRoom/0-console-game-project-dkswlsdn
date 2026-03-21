using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Cannon : Weapon
{
    public Cannon(Scene scene) : base(scene, true)
    {
    }

    public override void Fire(Point dir)
    {
        new CannonBullet(Scene, OwnerID, OwnerID.BulletPoint, dir);
    }
}