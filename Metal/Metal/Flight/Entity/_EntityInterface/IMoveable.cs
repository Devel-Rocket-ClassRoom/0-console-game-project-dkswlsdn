using System;
using System.Collections.Generic;
using System.Text;

public interface IMoveable
{
    Point ForwardPosition { get; }
    Point BackwardPosition { get; }
    void Move();
}