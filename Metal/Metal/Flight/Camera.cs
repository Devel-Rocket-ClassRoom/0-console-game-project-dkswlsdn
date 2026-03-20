using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : GameObject
{
    public static Point Position;
    public Camera(Scene scene, Point position) : base(scene)
    {
        Position = position;
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
    }
}