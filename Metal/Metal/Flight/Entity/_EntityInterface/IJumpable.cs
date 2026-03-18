using System;
using System.Collections.Generic;
using System.Text;

public interface IJumpable
{
    bool IsOnGround { get; set; }
    int JumpForce { get; }
    void Jump(float deltaTime);
    void VirticalMove(int force);

    Point GetNextPosition(int lowerForce);
}