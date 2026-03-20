using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(Scene scene, Entity id, Point point, Point direction) : base(scene, id, point, direction)
    {
        _runningDirection = new Point(direction);
        RectAngle = new RectAngle(this, (-8, 0), (8, 45));

        _bulletSpeed = 0;
        _life = 0.2f;
        _damage = 20;

        _interval = 0.3f;
        _isOnlyTarget = false;

        _currentPixels = _idelPixels;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position, Position.ToString());
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        //RectAngle.Follow();
    }

    public string[] _idelPixels =
    {
        "GGGGGGGGGGGGGGGGG",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
        "        G        ",
    };
}
