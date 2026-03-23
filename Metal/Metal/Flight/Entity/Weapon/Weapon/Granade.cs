using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Granade : Weapon
{
    static public int granadeCount = 0;

    public Granade(GameScene scene, CharacterEntity id) : base(scene, true)
    {
        Name = "Granade";
        Arms = 10;
        _recoil = 0.2f;
        Owner = id;
    }

    public override float Fire(Point dir)
    {
        if (granadeCount >= 2 || Arms <= 0) return 0;

        Scene.AddGameObject(new GranadeBullet((GameScene)Scene, Owner.BulletPoint, dir));
        Arms--;
        granadeCount++;
        return _recoil;
    }
}