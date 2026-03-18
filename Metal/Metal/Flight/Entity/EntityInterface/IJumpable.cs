using System;
using System.Collections.Generic;
using System.Text;

public interface IJumpable
{
    bool IsOnGround { get; set; }
    int JumpForce { get; }
    float BlockPerSecond { get; }
    void Jump();
}