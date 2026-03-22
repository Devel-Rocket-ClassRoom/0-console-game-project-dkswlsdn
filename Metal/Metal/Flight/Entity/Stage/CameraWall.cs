using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class CameraWall : Entity
{
    public CameraWall(Scene scene, Point point) : base(scene, point, false)
    {
        Type = EntityType.Ground;
        Mask = EntityType.Player;

        Width = 1;
        Height = 1;
        _currentPixels = new string[] { "G" };
    }
}