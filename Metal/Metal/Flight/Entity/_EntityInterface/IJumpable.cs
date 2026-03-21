using System;
using System.Collections.Generic;
using System.Text;

public interface IJumpable
{
    Point GroundChecker { get; }
    float VirtlcalVelocity { set; }
    bool IsLand { get; set; }

    void Jump(float deltaTime);
    void VirticalMove();
}