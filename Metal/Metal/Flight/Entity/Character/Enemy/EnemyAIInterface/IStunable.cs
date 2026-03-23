using System;
using System.Collections.Generic;
using System.Text;

public interface IStunable
{
    float LeftStunDuration { get; }
    float MaxStunDuration { get; }

    void DoStun(float deltaTime);
    bool IsStunEnd();
}