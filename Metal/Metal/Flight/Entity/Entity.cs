using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public abstract class Entity : GameObject
{
    protected Point _position;

    public int ID { get; private set; }
    public int Health { get; set; }


    public Entity(Scene scene) : base(scene)
    {
    }


}