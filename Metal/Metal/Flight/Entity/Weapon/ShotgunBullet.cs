using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(Scene scene, Entity id, Point point, Point aim) : base(scene, id, point + Point.DirectionConverter((23, 0), aim), aim)
    {
        RectAngle = new RectAngle(this, (46, 17));

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

    protected override void Go()
    {
    }

    public string[] _idelPixels =
    {
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
        "                                            G",
    };
}
