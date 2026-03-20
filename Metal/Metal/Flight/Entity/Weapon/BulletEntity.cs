using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class BulletEntity : AttackEntity
{
    protected bool _isOnlyTarget = false;
    protected int _bulletSpeed;

    protected int _width;
    protected int _height;

    public BulletEntity(Scene scene, CharacterEntity id, Point point, Point aim) : base(scene, id, point)
    {
        Direction = aim;
        Range = 100;
        _life = 1f;
    }

    public override void Update(float deltaTime)
    {
        Go();
        RectAngle.Follow();

        base.Update(deltaTime);

        _life -= deltaTime;

        if (_life <= 0)
        {
            Scene.RemoveGameObject(this);
        }
    }

    protected abstract void Go();

    protected override void AfterDealDamage()
    {
        if (Scene is GameScene g)
        {
            if (_isOnlyTarget)
                g.RemoveGameObject(this);
        }
    }
}