using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : Entity
{
    public List<Point> GroundPosition;

    public Ground(Scene scene, int id) : base(scene, id)
    {
        GroundPosition = new List<Point>();

        for (int i = -800; i < 800; i++)
        {
            GroundPosition.Add((i, 2));
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.FillRect(Position.WinXY.X, Position.WinXY.Y, )
    }

    public override void Update(float deltaTime)
    {
    }
}