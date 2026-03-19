using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class BulletEntity : AttackEntity
{
    protected bool _isOnlyTarget = false;
    protected (int x, int y) _direction;
    protected int _bulletSpeed;
    protected float _life;

    public BulletEntity(Scene scene, Entity id, Point point, int damage, int bulletSpeed, Point direction) : base(scene, id, point, damage)
    {
        _direction = direction;
        _bulletSpeed = bulletSpeed;

        Range = 10;
        _life = 1f;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _life -= deltaTime;

        if (_life <= 0)
        {
            Scene.RemoveGameObject(this);
        }

        Go();
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
    }

    protected virtual void Go()
    {
        Position.X += _bulletSpeed * _direction.x;
    }

    protected override void AfterDealDamage()
    {
        if (Scene is GameScene g)
        {
            if (_isOnlyTarget)
                g.RemoveGameObject(this);
        }
    }
}