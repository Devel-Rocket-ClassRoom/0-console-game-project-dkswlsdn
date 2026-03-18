using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public struct Point
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator +(Point a, (int x, int y) b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static implicit operator Point((int x, int y) tuple)
    {
        return new Point(tuple.x, tuple.y);
    }

    public static implicit operator (int, int)(Point p)
    {
        return (p.x, p.y);
    }
}