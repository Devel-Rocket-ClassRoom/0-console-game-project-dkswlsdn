using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class BulletEntity : Entity
{
    public int Damage;
    protected float _life;

    protected float _interval = 0; // 다단히트 방지
    protected List<Entity> _targetsBuffer = new List<Entity>(10);

    public int Range { get; protected set; }




    protected bool _isOnlyTarget = true;
    protected float _bulletSpeed;

    public BulletEntity(Scene scene, Point point, Point aim, int width, int height) : base(scene, point, true)
    {
        _canMove = true;

        Direction = aim;
        Range = 100;
        _life = 1f;

        if (aim.Y == 0)
        {
            Width = width;
            Height = height;
        }
        else
        {
            Width = height;
            Height = width;
        }

        ShotPositionAdjust(aim);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        CheckCameraBounds(true);
        CheckDynamicCollision();

        _life -= deltaTime;

        if (_life <= 0)
        {
            Scene.RemoveGameObject(this);
        }
    }

    protected virtual void CheckDynamicCollision()
    {
        if (Physics.IsOverrap(this, Scene.DynamicEntityList, out Entity collider))
        {
            if (collider != null && collider.IsActive)
            {
                collider.CollisionFromDynamic(ID, Damage);
                CollisionFromDynamic();
            }
        }
    }

    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
        Scene.AddGameObject(new HitEffect(Scene, Position));
        if (_isOnlyTarget) Destroy();
    }

    public override void CollisionToStatic()
    {
        Scene.AddGameObject(new HitEffect(Scene, Position));
        if (_isOnlyTarget) Destroy();
    }



    protected virtual void ShotPositionAdjust(Point aim)
    {
        float offsetX = (Width / 2.0f) * (aim.X - 1);
        float offsetY = (Height / 2.0f) * (aim.Y - 1);
        Position = new Point(Position.X + offsetX, Position.Y + offsetY);
    }








    public float GetDistanceSqToRect(Point pos, RectAngle rect)
    {
        float closestX = MathF.Max(rect.Min.X, MathF.Min(pos.X, rect.Max.X));
        float closestY = MathF.Max(rect.Min.Y, MathF.Min(pos.Y, rect.Max.Y));

        float dx = pos.X - closestX;
        float dy = pos.Y - closestY;

        return (dx * dx) + (dy * dy);
    }
}