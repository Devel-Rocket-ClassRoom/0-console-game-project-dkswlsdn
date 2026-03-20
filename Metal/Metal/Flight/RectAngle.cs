using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class RectAngle
{
    public Point Position { get; set; }
    private int _direction;
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

    public void SpinRect(int dir, bool isReverse)
    {
        int hw = Width / 2;
        int h = Height;

        if (dir == _direction)
            return;

        int delta = (dir - _direction + 4) % 4;

        switch (dir)
        {
            case 0:
                Rect.a = new Point(-hw, 0);
                Rect.b = new Point(hw, h);
                break;

            case 1:
                Rect.a = new Point(0, -hw);
                Rect.b = new Point(h, hw);
                break;

            case 2: 
                Rect.a = new Point(-hw, -h);
                Rect.b = new Point(hw, 0);
                break;

            case 3:
                Rect.a = new Point(-h, -hw);
                Rect.b = new Point(0, hw);
                break;
        }

        _direction = dir;
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

/*
 * 
 * 
 * 
 * 
 * 
 * * * * * * */