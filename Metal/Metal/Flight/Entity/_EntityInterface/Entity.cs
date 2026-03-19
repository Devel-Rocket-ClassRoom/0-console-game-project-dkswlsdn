using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public abstract class Entity : GameObject
{
    protected Point _position;
    public Point Position { get { return (_position.X * 2, _position.Y); } protected set { _position = (value.X / 2, value.Y); } }
    public RectAngle RectAngle { get; protected set; }

    public int ID { get; private set; }


    public Entity(Scene scene, int id) : base(scene)
    {
        ID = id;
    }
}