using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(Scene scene, int id, Point point, Point direction) : base(scene, id, point, 20, 0, direction)
    {
        RectAngle = new RectAngle(this, (0, -8), (45, 8)).SpinRect(direction);
        _interval = 0.3f;
        _life = 0.2f;
        _isOnlyTarget = false;
        Range = 45;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y, Position.ToString());
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        RectAngle.Follow();
    }
}
