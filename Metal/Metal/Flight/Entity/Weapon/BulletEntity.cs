using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class BulletEntity : AttackEntity
{
    public float Thickness { get { return _currentPixels.Length; } }
    public float Breadth { get { return _currentPixels[0].Length; } }



    protected bool _isOnlyTarget = false;
    protected float _bulletSpeed;

    public BulletEntity(Scene scene, CharacterEntity id, Point point, Point aim) : base(scene, id, point)
    {
        Direction = aim;
        Range = 100;
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
    }

    protected override void AfterHit()
    {
        new HitEffect(Scene, Position);

        if (Scene is GameScene g)
        {
            if (_isOnlyTarget)
                g.RemoveGameObject(this);
        }
    }
}