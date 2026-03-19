using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Framework.Engine;

public abstract class AttackEntity : Entity
{
    protected int _damage;
    protected List<CharacterEntity> _targetsBuffer = new List<CharacterEntity>(10);

    public int Range { get; protected set; }


    public AttackEntity(Scene scene, int id, int damage) :base(scene, id)
    {
        
    }

    public override void Update(float deltaTime)
    {
        DealDamage();
    }

    protected virtual void DealDamage()
    {
        List<CharacterEntity> allEntities = null;

        if (Scene is GameScene s)
        {
            allEntities = s.EntityList;
        }

        float rangeSq = Range * Range;

        for (int i = 0; i < allEntities?.Count; i++)
        {
            var target = allEntities[i];

            int dx = target.Position.X - Position.X;
            int dy = target.Position.Y - Position.Y;

            if (dx * dx + dy * dy <= rangeSq)
            {
                _targetsBuffer.Add(target);
            }
        }


        for (int i = 0; i < _targetsBuffer.Count; i++)
        {
            if (RectAngle.IsOverrap(_targetsBuffer[i].RectAngle))
            {
                _targetsBuffer[i].TakeDamage(ID, _damage, 100);
            }
        }
    }
}
