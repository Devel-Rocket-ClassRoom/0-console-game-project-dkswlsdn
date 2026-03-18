using System;
using System.Collections.Generic;
using System.Text;

public interface IAimable
{
    (int x, int y) AimDirection { get; set; }
    void Aiming();
}