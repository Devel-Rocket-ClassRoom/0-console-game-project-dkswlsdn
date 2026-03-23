using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Handgun : Weapon
{
    public Handgun(GameScene scene, CharacterEntity id, bool isMain = true) : base(scene, isMain)
    {
        Name = "Handgun";
        Arms = 1000;
        _recoil = 0.1f;
        Owner = id;
    }

    public override float Fire(Point dir)
    {
        Scene.AddGameObject(new HandgunBullet((GameScene)Scene, Owner.BulletPoint, dir));
        return _recoil;
    }
}