using System;
using System.Collections.Generic;
using System.Text;

using Framework.Engine;

public abstract class CharacterEntity : Entity, IHitable
{
    protected bool _isDead;

    public bool IsImmune { get; set; }
    public int Health { get; protected set; }

    public Dictionary<int, long> ImmunityList { get; } = new Dictionary<int, long>();
    // 체력과 피격, 전투관련

    public virtual Point BulletPoint { get { return Position; } }

    public Point Aim = (1, 0);



    protected float _motionTime = 1f;


    public CharacterEntity(Scene scene, Point point) : base(scene, point)
    {
        if (scene is GameScene g_scene)
        {
            g_scene.EntityList.Add(this);
        }

        Direction = (1, 0);
    }

    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (_isDead)
        {
            if (_motionTime > 0f)
            {
                _motionTime -= deltaTime;
                DeadMotion(deltaTime);
            }
            else
            {
                Scene.RemoveGameObject(this);
                IsActive = false;
            }
        }

        if (IsActive)
            RectAngle.Follow();
    }








    public virtual void DeadMotion(float deltaTime)
    {

    }
    public void TakeDamage(int attackId, int damage, int immuneDuration)
    {
        if (IsImmune) return;

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
            _isDead = true;
        }

        ImmunityList[attackId] = currentTime + immuneDuration;
    }
}