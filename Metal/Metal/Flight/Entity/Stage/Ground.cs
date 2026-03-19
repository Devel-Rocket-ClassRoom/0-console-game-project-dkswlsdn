using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GameObject
{
    public List<Point> GroundPosition;

    public Ground(Scene scene) : base(scene)
    {
        GroundPosition = new List<Point>();

        for (int i = -800; i < 800; i++)
        {
            GroundPosition.Add((i, 2));
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
    }
}