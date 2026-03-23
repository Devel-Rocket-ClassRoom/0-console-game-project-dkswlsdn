using System;
using System.Collections.Generic;
using System.Text;

public interface IDeadable
{
    float LeftDeadDuration { get; }
    float MaxDeadDuration { get; }

    void DoDead(float deltaTime);
    bool IsDeadEnd();
}
