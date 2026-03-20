using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GetWeapon : Entity
{
    protected int _arms;
    protected int _additionalArms;

    public GetWeapon(Scene scene, Point point, int arms, int additionalArms) : base(scene, point)
    {
        RectAngle = new RectAngle(this, (9, 9));

        _arms = arms;
        _additionalArms = additionalArms;
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
                        Get(p);

                        Scene.RemoveGameObject(this);
                    }
                }
            }
        }
    }

    protected abstract void Get(Player p);
    

    
}