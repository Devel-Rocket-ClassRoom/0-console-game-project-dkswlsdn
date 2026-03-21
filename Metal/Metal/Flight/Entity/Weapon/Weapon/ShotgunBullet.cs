using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(Scene scene, CharacterEntity id, Point point, Point aim) : base(scene, id, point + new Point(23, 0).PointConverter(aim), aim)
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

        RectAngle.Follow(Position);
    }

    protected override void AfterHit()
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
