using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class RectAngle : Entity
{
    private Entity _chase;
    private List<(Point a, Point b)> _rect = new List<(Point a, Point b)>();
    // a : 좌하단
    // b : 우상단

    public RectAngle(Scene scene, Entity chase, params List<(Point a, Point b)> rect) : base(scene, chase.ID)
    {
        _chase = chase;

        for (int i = 0; i < rect.Count; i++)
        {
            _rect.Add(rect[i]);
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        for (int i = 0; i < _rect.Count; i++)
        {
            buffer.DrawBox(
                new Point(Position.X + _rect[i].a.X, 0).WinXY.X,
                new Point(0, Position.Y + _rect[i].a.Y).WinXY.Y,
                _rect[i].b.WinXY.X - _rect[i].a.WinXY.X + 2,
                _rect[i].b.WinXY.Y - _rect[i].a.WinXY.Y + 1,
                ConsoleColor.Blue, ConsoleColor.Black);
        }
    }

    public bool IsOverrap(RectAngle rectAngle)
    {
        for (int i = 0; i < _rect.Count; i++)
        {
            for (int j = 0; j < rectAngle._rect.Count; j++)
            {
                return !(_rect[i].b.X < rectAngle._rect[j].a.X || _rect[i].a.X > rectAngle._rect[j].b.X
                    || _rect[i].b.Y > rectAngle._rect[j].a.Y || _rect[i].a.Y < rectAngle._rect[j].b.Y);
            }
        }

        return false;
    }

    public override void Update(float deltaTime)
    {
        Position = _chase.Position;
    }
}