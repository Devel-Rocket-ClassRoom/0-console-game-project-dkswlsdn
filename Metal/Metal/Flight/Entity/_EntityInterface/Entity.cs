using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public abstract class Entity : GameObject
{
    public static int nextId = 1;

    public Point Position { get; set; }
    public RectAngle RectAngle { get; protected set; }

    public int ID { get; private set; }


    public Entity(Scene scene, Point point) : base(scene)
    {
        Position = point;
        ID = nextId++;
    }
}