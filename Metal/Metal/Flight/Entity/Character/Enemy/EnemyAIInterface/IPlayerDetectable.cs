using System;
using System.Collections.Generic;
using System.Text;

public interface IPlayerDetectable
{
    int DetectRange { get; }

    bool IsPlayerSuperNearing();
    bool IsPlayerDead();
    bool IsPlayerRebirth();
    bool IsPlayerInRange();
}