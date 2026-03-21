using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class CannonBullet : BulletEntity
{
    Point _runningDirection;

    public CannonBullet(Scene scene, CharacterEntity id, Point point, Point aim) : base(scene, id, point, (1, 0))
    {
        RectAngle = new RectAngle(this, (5, 5));

        _life = 10f;
        _bulletSpeed = 2f;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;

        _runningDirection = aim.Normalize();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        Move();
        RectAngle.Follow(Position);
    }

    private void Move()
    {
        Position += _runningDirection * _bulletSpeed;
    }

    private string[] _idelPixels =
    {
        " DDD ",
        "DDBRD",
        "DDRBD",
        "DDDDD",
        " DDD ",
    };

    
}