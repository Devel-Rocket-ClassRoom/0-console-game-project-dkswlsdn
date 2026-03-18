using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GameObject
{
    public List<(int x, int y)> GroundPosition;

    public Ground(Scene scene) : base(scene)
    {
        GroundPosition = new List<(int x, int y)>();
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
    }
}