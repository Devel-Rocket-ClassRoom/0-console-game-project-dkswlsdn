using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

    public class EnemyGranade : Weapon
    {
        public EnemyGranade(Scene scene, CharacterEntity id) : base(scene, true)
        {
            Name = "EnemyGranade";
            Arms = 10;
            _recoil = 0.2f;
            Owner = id;
        }

        public override float Fire(Point dir)
        {
            Scene.AddGameObject(new EnemyGranadeBullet((GameScene)Scene, Owner.BulletPoint, dir));
            return _recoil;
        }
    }