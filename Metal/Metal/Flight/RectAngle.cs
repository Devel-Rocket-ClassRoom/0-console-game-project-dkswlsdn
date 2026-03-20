using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class RectAngle
{
    public Point Position { get; set; }
    private Entity _chase;
    public (Point a, Point b) Rect;

    public int Width { get { return Rect.b.X - Rect.a.X + 1; } }
    public int Height { get { return Rect.b.Y - Rect.a.Y + 1; } }
    // Position 기준 상대 위치
    // a : 좌하단
    // b : 우상단

    public RectAngle(Entity chase, Point a, Point b)
    {
        _chase = chase;
        Position = chase.Position;
        Rect = (a, b);
    }

    public RectAngle(Point point, Point a, Point b)
    {
        Position = point;
        Rect = (a, b);
    }

    public RectAngle(Point a, Point b)
    {
        Position = (0, 0);
        Rect = (a, b);
    }


    public bool IsOverrap(RectAngle rectAngle)
    {
        return !(Position.X + Rect.b.X < rectAngle.Position.X + rectAngle.Rect.a.X 
            || Position.X + Rect.a.X > rectAngle.Position.X + rectAngle.Rect.b.X
            || Position.Y + Rect.b.Y < rectAngle.Position.Y + rectAngle.Rect.a.Y
            || Position.Y + Rect.a.Y > rectAngle.Position.Y + rectAngle.Rect.b.Y);
    }

    public bool IsOverrap(Point a, Point b)
    {
        return !(Position.X + Rect.b.X < a.X 
            || Position.X + Rect.a.X > b.X
            || Position.Y + Rect.b.Y < a.Y 
            || Position.Y + Rect.a.Y > b.Y);
    }

    public bool IsOverrap(Point point)
    {
        return !(point.X < Position.X + Rect.a.X 
            || point.X > Position.X + Rect.b.X 
            || point.Y < Position.Y + Rect.a.Y 
            || point.X > Position.Y + Rect.b.Y);
    }

    public void SpinRect(Point dir)
    {
        if (dir.Y == 1)
        {
            Rect = ((-Rect.b.Y, Rect.a.X), (-Rect.a.Y, Rect.b.X));
        }
        else if (dir.Y == -1)
        {
            Rect = ((Rect.a.Y, -Rect.b.X), (Rect.b.Y, -Rect.a.X));
        }
        else if (dir.X == -1)
        {
            Rect = ((-Rect.b.X, Rect.a.Y), (-Rect.a.X, Rect.b.Y));
        }
    }


    public int HeightDiff(Point point)
    {
        return Math.Abs(point.Y - Position.Y + Rect.b.Y);
    }

    public void Follow()
    {
        Position = _chase.Position;
    }

    public void DrawRectAngle(ScreenBuffer buffer)
    {
        buffer.DrawBox(Position + (Rect.a.X, Rect.b.Y), Width * 2, Height, bgColor: ConsoleColor.DarkGray);
    }
}