using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Granade : Weapon
{
    public Granade(Scene scene) : base(scene, true)
    {
    }

    public override void Fire(Point dir)
    {
        new GranadeBullet(Scene, OwnerID, OwnerID.BulletPoint, dir);
    }
}