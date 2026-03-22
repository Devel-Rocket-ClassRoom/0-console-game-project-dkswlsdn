using System;
using System.Collections.Generic;
using System.Text;

using Framework.Engine;

public abstract class CharacterEntity : Entity
{
    public bool IsAlive { get; private set; } = true;
    public bool IsImmune { get; set; }
    public int Health { get; protected set; }
    public Dictionary<int, long> ImmunityList { get; } = new Dictionary<int, long>();
    // 다단히트 방지
    protected float _recoil = 0f;


    public virtual Point BulletPoint { get { return Position; } }
    public Point Aim = (1, 0);





    public CharacterEntity(Scene scene, Point point) : base(scene, point, true)
    {
        Direction = (1, 0);
    }

    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    

    public void TakeDamage(int attackId, int damage)
    {
        if (IsImmune || damage == 0) return;

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

        if (Health <= 0)
        {
            IsAlive = false;
        }

        ImmunityList[attackId] = currentTime + 1000;
    }

    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
        TakeDamage(id, damage);
    }
}