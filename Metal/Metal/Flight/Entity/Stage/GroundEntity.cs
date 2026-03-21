using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GroundEntity : Entity
{
    public bool CanHitToLowerBullet;
    public bool CanHitToHigherBullet;

    public GroundEntity(Scene scene, Point point, bool bulletHit = true) : base(scene, point)
    {
        CanHitToLowerBullet = bulletHit;
    }
}
