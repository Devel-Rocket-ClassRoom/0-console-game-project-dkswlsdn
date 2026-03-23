using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Lazor : Weapon
{
    Boss _boss;

    public Lazor(Scene scene, Boss boss) : base(scene, true)
    {
        _boss = boss;
    }

    public override float Fire(Point dir)
    {
        throw new NotImplementedException();
    }

    public void Fire(Point position, bool i)
    {
        Scene.AddGameObject(new LazorBullet((GameScene)Scene, position, _boss));
    }
}