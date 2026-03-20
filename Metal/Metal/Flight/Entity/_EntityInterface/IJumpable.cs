using System;
using System.Collections.Generic;
using System.Text;

public interface IJumpable
{
    float VirtlcalVelocity { set; }
    void Jump(float deltaTime);
    void VirticalMove();
}