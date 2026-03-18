using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public interface IHealthHaveable
{
    int Health { get; }
    Dictionary<int, long> ImmunityList { get; }

    public (Point, Point) RectAngle { get; }

    void TakeDamage(int attackId, int damage, int immuneDuration);
}