using System;
using System.Collections.Generic;
using System.Text;

public interface IMoveable
{
    float LeftMoveTime { get; }
    float MaxMoveInterval { get; }

    void DoMove(float deltaTime);
    void ChangeDirection();

    bool IsMoveEnd();
    bool IsOutOfCamera();
}