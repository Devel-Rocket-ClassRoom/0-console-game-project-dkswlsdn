using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Cannon : Weapon
{
    public Cannon(Scene scene) : base(scene, true)
    {
    }

    public override float Fire(Point dir)
    {
        Scene.AddGameObject(new CannonBullet(Scene, Owner.BulletPoint, dir));
        return _recoil;
    }
}