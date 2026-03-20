using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class BulletEntity : AttackEntity
{
    protected bool _isOnlyTarget = false;
    protected int _bulletSpeed;

    protected int _width;
    protected int _height;

    public BulletEntity(Scene scene, Entity id, Point point, Point direction) : base(scene, id, point)
    {
        _runningDirection = new Point(direction);
        Direction = direction.ConvertToInt();
        Range = 100;
        _life = 1f;
    }

    public override void Update(float deltaTime)
    {
        Go();

        base.Update(deltaTime);

        _life -= deltaTime;

        if (_life <= 0)
        {
            Scene.RemoveGameObject(this);
        }
    }

    protected virtual void Go()
    {
        Position.X += _bulletSpeed * _runningDirection.X;
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