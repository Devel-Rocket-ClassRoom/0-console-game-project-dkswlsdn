using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public abstract class Entity : GameObject
{
    public Point Position { get; protected set; }

    public int ID { get; private set; }


    public Entity(Scene scene) : base(scene)
    {
        if (scene is GameScene g_scene)
        {
            g_scene.EntityList.Add(this);
        }
    }
}