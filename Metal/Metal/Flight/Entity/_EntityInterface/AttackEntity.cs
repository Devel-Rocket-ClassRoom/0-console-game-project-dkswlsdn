using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Framework.Engine;

public abstract class AttackEntity : Entity
{
    protected float _interval = 0;
    protected int ownerId;
    protected int _damage;
    protected List<CharacterEntity> _targetsBuffer = new List<CharacterEntity>(10);

    public int Range { get; protected set; }


    public AttackEntity(Scene scene, int id, Point point, int damage) :base(scene, point)
    {
        ownerId = id;
        _damage = damage;
    }

    public override void Update(float deltaTime)
    {
        DealDamage();
        UpdateFrame(deltaTime);
    }

    public virtual void UpdateFrame(float deltaTime) { }

    protected virtual void DealDamage()
    {
        List<CharacterEntity> allEntities = null;
        _targetsBuffer.Clear();

        if (Scene is GameScene g)
        {
            allEntities = g.EntityList;
        }

        float rangeSq = Range * Range;

        for (int i = 0; i < allEntities?.Count; i++)
        {
            var target = allEntities[i];

            int dx = target.Position.X - Position.X;
            int dy = target.Position.Y - Position.Y;

            if (dx * dx + dy * dy <= rangeSq && target.ID != ownerId)
            {
                _targetsBuffer.Add(target);
            }
        }


        for (int i = 0; i < _targetsBuffer.Count; i++)
        {
            if (RectAngle.IsOverrap(_targetsBuffer[i].RectAngle))
            {
                _targetsBuffer[i].TakeDamage(ID, _damage, (int)(_interval * 1000));
                AfterDealDamage();
            }
        }
    }

    protected virtual void AfterDealDamage() { }
}
