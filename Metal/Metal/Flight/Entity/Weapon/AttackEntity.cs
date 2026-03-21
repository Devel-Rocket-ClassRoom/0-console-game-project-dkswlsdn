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
    protected List<Entity> _targetsBuffer = new List<Entity>(10);

    public int Range { get; protected set; }


    public AttackEntity(Scene scene, CharacterEntity id, Point point) :base(scene, point)
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
        List<Entity> allEntities = null;
        _targetsBuffer.Clear();

        if (Scene is GameScene g)
        {
            allEntities = new List<Entity>();

            for (int i = 0; i < g.EntityList.Count; i++) allEntities.Add(g.EntityList[i]);
            for (int i = 0; i < g.GroundEntitiyList.Count; i++) allEntities.Add(g.GroundEntitiyList[i]);
            for (int i = 0; i < g.WallEntitiyList.Count; i++) allEntities.Add(g.WallEntitiyList[i]);
        }

        float rangeSq = Range * Range;

        for (int i = 0; i < allEntities?.Count; i++)
        {
            var target = allEntities[i];

            if (GetDistanceSqToRect(Position, target.RectAngle) <= rangeSq && target.ID != ownerId.ID && target.IsActive)
            {
                _targetsBuffer.Add(target);
            }
        }


        for (int i = 0; i < _targetsBuffer.Count; i++)
        {
            if (RectAngle.IsOverrap(_targetsBuffer[i].RectAngle))
            {
                if (_targetsBuffer[i] is CharacterEntity c)
                {
                    c.TakeDamage(ID, _damage, (int)(_interval * 1000));
                    AfterHit();
                }
                else if (_targetsBuffer[i] is GroundEntity st)
                {
                    if (st.CanHitToBullet)
                    {
                        AfterHit();
                    }
                }
            }
        }
    }

    public float GetDistanceSqToRect(Point pos, RectAngle rect)
    {
        float closestX = MathF.Max(rect.Min.X, MathF.Min(pos.X, rect.Max.X));
        float closestY = MathF.Max(rect.Min.Y, MathF.Min(pos.Y, rect.Max.Y));

        float dx = pos.X - closestX;
        float dy = pos.Y - closestY;

        return (dx * dx) + (dy * dy);
    }

    protected virtual void AfterHit() { }
}
