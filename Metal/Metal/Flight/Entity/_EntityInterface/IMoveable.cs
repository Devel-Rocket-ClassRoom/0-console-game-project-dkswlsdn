using System;
using System.Collections.Generic;
using System.Text;

public interface IMoveable
{
    Point NextPosition { get; }
    void Move();
}