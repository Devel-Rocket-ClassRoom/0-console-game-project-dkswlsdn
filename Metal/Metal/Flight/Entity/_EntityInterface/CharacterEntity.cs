using System;
using System.Collections.Generic;
using System.Text;

using Framework.Engine;

public abstract class CharacterEntity : Entity
{
    public int Health { get; protected set; }
    protected Dictionary<int, long> ImmunityList { get; } = new Dictionary<int, long>();

    public CharacterEntity(Scene scene, Point point) : base(scene, point)
    {
        if (scene is GameScene g_scene)
        {
            g_scene.EntityList.Add(this);
        }
    }

    public virtual void TakeDamage(int attackId, int damage, int immuneDuration)
    {
        temp();

        long currentTime = Environment.TickCount64;

        if (ImmunityList.TryGetValue(attackId, out long endTime))
        {
            if (currentTime < endTime)
            {
                return;
            }

            ImmunityList.Remove(attackId);
        }

        Health -= damage;
        ImmunityList[attackId] = currentTime + immuneDuration;
    }

    protected virtual void temp()
    {

    }
}