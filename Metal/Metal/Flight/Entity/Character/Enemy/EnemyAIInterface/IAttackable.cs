using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public interface IAttackable
{
    float LeftAttackCooldown { get; }
    float MaxAttackCooldown { get; }
    void DoAttack(float deltaTime);

    bool IsAttackEnd();
}