using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Framework.Engine;

public abstract class AttackEntity : Entity
{
    protected int _damage;
    protected float _life;

    protected float _interval = 0; // 다단히트 방지
    protected Entity ownerId; // 피아구분
    protected List<CharacterEntity> _targetsBuffer = new List<CharacterEntity>(10);

    public int Range { get; protected set; }


    public AttackEntity(Scene scene, Entity id, Point point) :base(scene, point)
    {
        ownerId = id;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        DealDamage();
    }

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

            int dx = (int)(target.Position.X - Position.X);
            int dy = (int)(target.Position.Y - Position.Y);

            if (dx * dx + dy * dy <= rangeSq && target.ID != ownerId.ID && target.IsActive)
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
