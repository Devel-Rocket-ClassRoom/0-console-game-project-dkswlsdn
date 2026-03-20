using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GroundEntity : Entity
{
    public GroundEntity(Scene scene, Point point) : base(scene, point)
    {
        if (Scene is GameScene g)
        {
            g.GroundEntitiyList.Add(this);
            g.GroundEntitiyList.Sort((a, b) => a.Position.Y.CompareTo(b.Position.Y));
        }
    }
}
