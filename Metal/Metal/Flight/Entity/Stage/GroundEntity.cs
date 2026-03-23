using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GroundEntity : Entity
{
    public GroundEntity(GameScene scene, Point point, bool isSmall = true) : base(scene, point, false)
    {
        Type = EntityType.Ground;
        Mask = EntityType.Player | EntityType.Enemy | EntityType.Bullet | EntityType.Trigger;

        if (isSmall) { Width = 1; Height = 1; }
        else { Width = 5; Height = 5; }
        _currentPixels = new string[] { "GGGGG", "GGGGG", "GGGGG", "GGGGG", "GGGGG", };
    }
}
