using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public interface IHitable
{
    bool IsImmune { get; }
    int Health { get; }
    Dictionary<int, long> ImmunityList { get; }


    void TakeDamage(int attackId, int damage, int immuneDuration);
}