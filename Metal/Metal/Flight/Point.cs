using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class Point
{
    public int X;
    public int Y;

    public Point WinXY { get { return (X * 2, -Y + ShottingGame.k_Height - 1); } set{ X = value.X / 2; Y = -value.Y + ShottingGame.k_Height - 1; } }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }


    public static Point operator +(Point a, (int x, int y) b)
    {
        return new Point(a.X + b.x, a.Y + b.y);
    }

    public static implicit operator Point((int x, int y) tuple)
    {
        return new Point(tuple.x, tuple.y);
    }

    public static implicit operator (int, int)(Point p)
    {
        return (p.X, p.Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}