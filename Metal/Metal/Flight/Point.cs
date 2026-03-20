using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public struct Point
{
    public float X;
    public float Y;

    public Point((float x, float y) point)
    {
        X = point.x;
        Y = point.y;
    }

    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }


    

    public static Point operator *(Point a, int b)
    {
        return new Point(a.X * b, a.Y * b);
    }

    public static Point operator +(Point a, (float x, float y) b)
    {
        return new Point(a.X + b.x, a.Y + b.y);
    }

    public static Point operator -(Point a, (float x, float y) b)
    {
        return new Point(a.X - b.x, a.Y - b.y);
    }





    public static implicit operator Point((float x, float y) tuple)
    {
        return new Point(tuple.x, tuple.y);
    }

    public static implicit operator (float, float)(Point p)
    {
        return (p.X, p.Y);
    }

    public static implicit operator Point((int x, int y) tuple)
    {
        return new Point(tuple.x, tuple.y);
    }

    public static implicit operator (int, int)(Point p)
    {
        return ((int)p.X, (int)p.Y);
    }

    




    public override string ToString()
    {
        return $"({X}, {Y})";
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

    public static Point DirectionConverter(Point amount, Point dir)
    {
        switch ((dir.X, dir.Y))
        {
            default:
            case (1, 0):
                return amount;

            case (-1, 0):
                return (-amount.X, amount.Y);

            case (0, 1):
                return (amount.Y, amount.X);

            case (0, -1):
                return (amount.Y, -amount.X);
        }
    }
}