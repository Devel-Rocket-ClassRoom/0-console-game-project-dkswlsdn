using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Framework.Engine;

public abstract class AttackEntity : GameObject
{
    protected int _ownerId;
    protected int _attackId;

    protected int _damage;
    protected List<Point> _hitbox;
    protected List<Entity> _targetsBuffer = new List<Entity>(10);

    public int Range { get; }


    public AttackEntity(Scene scene, Point position, int ownerId, int damage) :base(scene)
    {
        SetHitbox(position);
    }

    public override void Update(float deltaTime)
    {
        DealDamage();
    }

    protected abstract void SetHitbox(Point position);

    protected virtual void DealDamage()
    {
        List<Entity> allEntities = null;

        if (Scene is GameScene s)
        {
            allEntities = s.EntityList;
        }

        float rangeSq = Range * Range;

        for (int i = 0; i < allEntities?.Count; i++)
        {
            var target = allEntities[i];

            int dx = target.Position.x - _hitbox[0].x;
            int dy = target.Position.y - _hitbox[0].y;

            if (dx * dx + dy * dy <= rangeSq)
            {
                _targetsBuffer.Add(target);
            }
        }
    }
}
