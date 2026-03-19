using System;
using System.Collections.Generic;
using System.Text;

using Framework.Engine;

public abstract class CharacterEntity : Entity
{
    public int Health { get; }
    protected Dictionary<int, long> ImmunityList { get; }

    public CharacterEntity(Scene scene, int id) : base(scene, id)
    {
        if (scene is GameScene g_scene)
        {
            g_scene.EntityList.Add(this);
        }
    }

    public abstract void TakeDamage(int attackId, int damage, int immuneDuration);

}