using System;
using System.Collections.Generic;
using System.Text;

public interface IMoveable
{
    (Point a, Point b) ForwardPosition { get; }
    (Point a, Point b) BackwardPosition { get; }
    void Move();
}