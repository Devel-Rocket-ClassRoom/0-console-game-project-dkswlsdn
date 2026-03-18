using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class RectAngle
{
    private Point pointA;
    private Point pointB;

    public bool IsOverrap(RectAngle rect)
    {
        return !(pointB.x < rect.pointA.x || pointA.y < rect.pointB.y || pointA.x < rect.pointB.x || pointB.y < rect.pointA.y);
    }
}