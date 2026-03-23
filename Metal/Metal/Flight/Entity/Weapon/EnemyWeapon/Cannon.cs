using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Cannon : Weapon
{
    public Cannon(Scene scene, CharacterEntity owner) : base(scene, true)
    {
        Owner = owner;
    }

    public override float Fire(Point dir)
    {
        Scene.AddGameObject(new CannonBullet((GameScene)Scene, Owner.BulletPoint, dir));
        return _recoil;
    }

    public float Fire(Point dir, Point position)
    {
        Scene.AddGameObject(new CannonBullet((GameScene)Scene, position, dir));
        return _recoil;
    }
}