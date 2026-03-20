using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class Point
{
    public int X;
    public int Y;

    public Point((int x, int y) point)
    {
        X = point.x;
        Y = point.y;
    }

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

    public static Point operator *(Point a, int b)
    {
        return new Point(a.X * b, a.Y * b);
    }

    public static Point operator *(Point a, Point b)
    {
        return new Point(a.X * b.X, a.Y * b.Y);
    }


    public static Point operator +(Point a, (int x, int y) b)
    {
        return new Point(a.X + b.x, a.Y + b.y);
    }

    public static Point operator -(Point a, (int x, int y) b)
    {
        return new Point(a.X - b.x, a.Y - b.y);
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

    public void ConvertFromInt(int i)
    {
        switch (i)
        {
            case 0: X = 0; Y = 1; break;
            case 1: X = 1; Y = 0; break;
            case 2: X = 0; Y = -1; break;
            case 3: X = -1; Y = 0; break;
        }
    }

    public int ConvertToInt()
    {
        switch ((X, Y))
        {
            case (0, 1):
                return 0;
            case (1, 0):
                return 1;
            case (0, -1):
                return 2;
            case (-1, 0):
                return 3;
            default:
                throw new Exception();
        }
    }
}