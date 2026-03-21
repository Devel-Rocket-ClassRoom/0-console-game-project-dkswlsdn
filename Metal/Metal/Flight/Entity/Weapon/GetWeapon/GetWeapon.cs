using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GetWeapon : Entity
{
    protected int _arms;
    protected Weapon weapon;


    public GetWeapon(Scene scene, Point point, int arms) : base(scene, point)
    {
        RectAngle = new RectAngle(this, (9, 9));

        _arms = arms;
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
                        if (p.mainWeapon.Name == Name)
                        {
                            p.mainWeapon.Arms += _arms;
                        }
                        else
                        {
                            weapon.OwnerID = p;
                            p.mainWeapon.Drop();
                            p.mainWeapon = weapon;
                            weapon.Arms = _arms;
                            Scene.AddGameObject(weapon);
                        }

                        Scene.RemoveGameObject(this);
                    }
                }
            }
        }
    }

    

    
}