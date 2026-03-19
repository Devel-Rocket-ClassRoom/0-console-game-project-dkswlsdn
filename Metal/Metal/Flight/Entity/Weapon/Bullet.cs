using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Bullet : AttackEntity
{
    private (int x, int y) _direction;
    private int _bulletSpeed;
    private float _life;

    public Bullet(Scene scene, Point point, int damage, int bulletSpeed, Point direction) : base(scene, point, damage)
    {
        _direction = direction;
        _bulletSpeed = bulletSpeed;

        Range = 10;

        RectAngle = new RectAngle(this, ((0, 0), (0, 0)));
        _life = 1f;
    }


    public override void Update(float deltaTime)
    {
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

    private void Go()
    {
        Position.X += _bulletSpeed * _direction.x;
    }
}