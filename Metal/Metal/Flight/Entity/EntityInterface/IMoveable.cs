using System;
using System.Collections.Generic;
using System.Text;

public interface IMoveable
{
    (int x, int y) NextPosition { get; }
    void Move();
}