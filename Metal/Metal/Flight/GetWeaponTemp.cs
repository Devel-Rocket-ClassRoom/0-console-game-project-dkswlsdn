using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GetWeaponTemp : Entity
{
    public GetWeaponTemp(Scene scene, Point point) : base(scene, point)
    {
        RectAngle = new RectAngle((100, 20), (-3, 0), (3, 6));
        _currentPixels = _idelPixels;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.EntityList.Count; i++)
            {
                if (g.EntityList[i] is Player p)
                {
                    if (RectAngle.IsOverrap(p.RectAngle))
                    {
                        if (p.mainWeapon.Name == "Shotgun")
                        {
                            p.mainWeapon.Arms += 10;
                        }
                        else
                        {
                            p.mainWeapon.Drop();
                            p.mainWeapon = new Shotgun(Scene, p);
                            Scene.AddGameObject(p.mainWeapon);
                        }

                        Scene.RemoveGameObject(this);
                    }
                }
            }
        }
    }

    private string[] _idelPixels =
    {
        "AAAAAAA",
        "AYYYYYA",
        "AYAAAAA",
        "AYYYYYA",
        "AAAAAYA",
        "AYYYYYA",
        "AAAAAAA",
    };
}