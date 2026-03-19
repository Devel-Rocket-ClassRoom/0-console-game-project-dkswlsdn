using System;
using System.Collections.Generic;
using System.Text;

public interface IJumpable
{
    int JumpForce { get; set; }
    void Jump(float deltaTime);
    void VirticalMove(int force);

    Point GetNextPosition(int lowerForce);
}