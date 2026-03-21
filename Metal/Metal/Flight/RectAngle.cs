using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class RectAngle
{
    private readonly float _origWidth;
    private readonly float _origHeight;

    // 기준점(좌하단)을 필드로 가집니다.
    public Point Pibot { get; set; }

    private Entity _chase;

    public float Width { get; private set; }
    public float Height { get; private set; }

    public Point Min => Pibot;
    public Point Max => new Point(Pibot.X + Width - 1, Pibot.Y + Height - 1);
    public Point MinX_MaxY => (Min.X, Max.Y);
    public Point MaxX_MinY => (Max.X, Min.Y);

    public Point Position
    {
        get
        {
            return new Point(Pibot.X + (Width - 1) / 2f, Pibot.Y + (Height - 1) / 2f);
        }
        set
        {
            float halfW = (Width - 1) / 2f;
            float halfH = (Height - 1) / 2f;
            Pibot = new Point(value.X - halfW, value.Y - halfH);
        }
    }

    public RectAngle(Entity chase, Point size)
    {
        _chase = chase;
        _origWidth = size.X;
        _origHeight = size.Y;

        Width = _origWidth;
        Height = _origHeight;
        Position = chase.Position;
    }





    public bool IsOverrap(RectAngle rect)
    {
        return !(Min.X > rect.Max.X
            || Max.X < rect.Min.X
            || Min.Y > rect.Max.Y
            || Max.Y < rect.Min.Y);
    }

    public bool IsOverrap(Point a, Point b)
    {
        float otMinX = MathF.Min(a.X, b.X);
        float otMaxX = MathF.Max(a.X, b.X);
        float otMinY = MathF.Min(a.Y, b.Y);
        float otMaxY = MathF.Max(a.Y, b.Y);

        return !(Min.X > otMaxX
            || Max.X < otMinX
            || Min.Y > otMaxY
            || Max.Y < otMinY);
    }



    public void SpinRect((int X, int Y) dir)
    {
        switch (dir)
        {
            case (1, 0):
            case (-1, 0):
                Width = _origWidth;
                Height = _origHeight;
                break;

            case (0, 1):
            case (0, -1):
                Width = _origHeight;
                Height = _origWidth;
                break;
        }
    }

    public void SitDown()
    {
        Height /= 2;
    }
    public void WakeDown()
    {
        Height *= 2;
    }




    public void Follow()
    {
        Position = _chase.Position;
    }

    public void DrawRectAngle(ScreenBuffer buffer)
    {
        int drawMinX = (int)MathF.Round(Min.X);
        int drawMaxY = (int)MathF.Round(Max.Y);

        buffer.SetCell(Position, ConsoleColor.Red);
        buffer.DrawBox((drawMinX, drawMaxY), (int)Width, (int)Height, bgColor: ConsoleColor.DarkGray);
    }
}
