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

    public  Point DirectionConverter(Point dir)
    {
        switch ((dir.X, dir.Y))
        {
            default:
            case (1, 0):
                return (X, Y);

            case (-1, 0):
                return (-X, Y);

            case (0, 1):
                return (Y, X);

            case (0, -1):
                return (Y, -X);
        }
    }

    public Point DirectionConverter(Point prev, Point dest, out bool isReversed)
    {
        var key = (prev.X, prev.Y, dest.X, dest.Y);

        switch (key)
        {
            // 1. 상 -> 우 (원본)
            case (0, 1, 1, 0):
                isReversed = false;
                return new Point(X, Y);

            // 2. 우 -> 상 (x = y 대칭)
            case (1, 0, 0, 1):
                isReversed = false;
                return new Point(Y, X);

            // 3. 우 -> 하 (x = y 대칭 후, x축 대칭)
            // (x, y) -> (y, x) -> (y, -x)
            case (1, 0, 0, -1):
                isReversed = false;
                return new Point(Y, -X);

            // 4. 하 -> 우 (x축 대칭)
            // (x, y) -> (x, -y)
            case (0, -1, 1, 0):
                isReversed = true;
                return new Point(X, -Y);

            // 5. 상 -> 좌 (y축 대칭)
            // (x, y) -> (-x, y)
            case (0, 1, -1, 0):
                isReversed = false;
                return new Point(-X, Y);

            // 6. 좌 -> 상 (x = y 대칭 후, y축 대칭)
            // (x, y) -> (y, x) -> (-y, x)
            case (-1, 0, 0, 1):
                isReversed = true;
                return new Point(-Y, X);

            // 7. 좌 -> 하 (x = y 대칭 후, x = -y 대칭)
            // (x, y) -> (y, x) -> (-x, -y)
            case (-1, 0, 0, -1):
                isReversed = true;
                return new Point(-Y, -X);

            // 8. 하 -> 좌 (x = -y 대칭)
            // (x, y) -> (-y, -x)
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
}