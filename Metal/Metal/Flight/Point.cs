using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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


    

    public static Point operator *(Point a, float b)
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




    public static bool operator == (Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
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

    public Point PointConverter(Point dest)
    {
        switch ((dest.X, dest.Y))
        {
            case (1, 0):
                return this;

            case (-1, 0):
                return new Point(-X, Y);

            case (0, 1):
                return new Point(Y, X);

            case (0, -1):
                return new Point(Y, -X);

            default:
                return this;
        }
    }

    public Point DirectionConverter(Point prev, Point dest, out bool isReversed)
    {
        var key = (prev.X, prev.Y, dest.X, dest.Y);

        switch (key)
        {
            case (0, 1, 1, 0):
                isReversed = false;
                return new Point(X, Y);

            case (1, 0, 0, 1):
                isReversed = false;
                return new Point(Y, X);

            case (1, 0, 0, -1):
                isReversed = false;
                return new Point(Y, -X);

            case (0, -1, 1, 0):
                isReversed = true;
                return new Point(X, -Y);

            case (0, 1, -1, 0):
                isReversed = false;
                return new Point(-X, Y);

            case (-1, 0, 0, 1):
                isReversed = true;
                return new Point(-Y, X);

            case (-1, 0, 0, -1):
                isReversed = true;
                return new Point(-Y, -X);

            case (0, -1, -1, 0):
                isReversed = true;
                return new Point(-X, -Y);

            default:
                isReversed = false;
                return this;
        }
    }

    public Point Normalize()
    {
        float length = MathF.Sqrt(X * X + Y * Y);

        if (length < float.Epsilon)
        {
            return new Point(0, 0);
        }

        return new Point(X / length, Y / length);
    }

    public Point HexaNormalize()
    {
        float length = MathF.Sqrt(X * X + Y * Y);
        if (length < float.Epsilon) return new Point(0, 0);

        float radians = MathF.Atan2(Y, X);

        float step = MathF.PI / 8f;
        float index = MathF.Round(radians / step);

        float finalAngle = index * step;

        return new Point(
            MathF.Cos(finalAngle),
            MathF.Sin(finalAngle)
        );
    }

    public bool IsInDistance(Point target, float range)
    {
        float dx = target.X - X;
        float dy = target.Y - Y;

        float distanceSquared = (dx * dx) + (dy * dy);

        float rangeSquared = range * range;

        return distanceSquared <= rangeSquared;
    }
}