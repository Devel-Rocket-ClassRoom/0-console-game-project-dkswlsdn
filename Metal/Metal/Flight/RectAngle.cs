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

    public RectAngle(Entity chase, (Point a, Point b) rect)
    {
        _chase = chase;
        Position = chase.Position;
        Rect = rect;
    }


    public bool IsOverrap(RectAngle rectAngle)
    {
        return !(Rect.b.X < rectAngle.Rect.a.X || Rect.a.X > rectAngle.Rect.b.X
            || Rect.b.Y < rectAngle.Rect.a.Y || Rect.a.Y > rectAngle.Rect.b.Y);
    }

    public bool IsOverrap(Point a, Point b)
    {
        return !(Position.X + Rect.b.X < a.X || Position.X + Rect.a.X > b.X
            || Position.Y + Rect.b.Y < a.Y || Position.Y + Rect.a.Y > b.Y);
    }

    public bool IsOverrap(Point point)
    {
        return !(point.X < Rect.a.X || point.X > Rect.b.X || point.Y < Rect.a.Y || point.X > Rect.b.Y);
    }

    public int HeightDiff(Point point)
    {
        return Math.Abs(point.Y - Rect.b.Y);
    }

    public void Follow()
    {
        Position = _chase.Position;
    }

    public void DrawRectAngle(ScreenBuffer buffer)
    {
        buffer.DrawBox(new Point(Position.X + Rect.a.X, 0).WinXY.X, new Point(0, Position.Y + Rect.b.Y).WinXY.Y, Width * 2, Height, bgColor: ConsoleColor.DarkGray);
    }
}