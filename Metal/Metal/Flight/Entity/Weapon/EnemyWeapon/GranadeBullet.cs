using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;


public class GranadeBullet : BulletEntity
{
    Point _runningDirection;

    public GranadeBullet(Scene scene, CharacterEntity id, Point point, Point aim) : base(scene, id, point, (1, 0))
    {
        RectAngle = new RectAngle(this, (5, 5));

        _life = 10f;
        _bulletSpeed = 2f;
        _damage = 1;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;

        _runningDirection = aim.Normalize();
        _useGravity = true;
        _gravity = 2f;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (IsLand) AfterHit();

        Move();
        RectAngle.Follow(Position);
    }

    private void Move()
    {
        Position += _runningDirection * _bulletSpeed;
    }

    private string[] _idelPixels =
    {
        "  DD ",
        " DBRD",
        "DDRBD",
        "DDDD ",
        " DD  ",
    };


}