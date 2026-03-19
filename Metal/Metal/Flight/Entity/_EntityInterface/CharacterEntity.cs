using System;
using System.Collections.Generic;
using System.Text;

using Framework.Engine;

public abstract class CharacterEntity : Entity
{
    public int Health { get; protected set; }
    protected Dictionary<int, long> ImmunityList { get; }

    public CharacterEntity(Scene scene, Point point) : base(scene, point)
    {
        if (scene is GameScene g_scene)
        {
            g_scene.EntityList.Add(this);
        }
    }

    public abstract void TakeDamage(int attackId, int damage, int immuneDuration);

}