using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;


public class PlatformEntity : Entity
{
    public PlatformEntity(Scene scene, Point point) : base(scene, point, false)
    {
        Type = EntityType.Platform;
        Mask = EntityType.Player | EntityType.Enemy | EntityType.Trigger;

        Width = 5;
        Height = 5;
        _currentPixels = new string[]
        {
            "BBBBB",
            "BBBBB",
            "BBBBB",
            "BBBBB",
            "BBBBB",
        };
    }
}