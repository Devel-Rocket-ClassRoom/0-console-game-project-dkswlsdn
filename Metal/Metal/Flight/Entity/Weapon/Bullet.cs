using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Bullet : AttackEntity
{
    private Point _direction;
    private int _bulletSpeed;

    public Bullet(Scene scene, Point point, int damage, int bulletSpeed, Point direction) : base(scene, point, damage)
    {
        _direction = direction;
        _bulletSpeed = bulletSpeed;

        Range = 10;

        RectAngle = new RectAngle(this, ((0, 0), (0, 0)));
    }


    public override void Update(float deltaTime)
    {
        Position.Y += _bulletSpeed;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
    }
}