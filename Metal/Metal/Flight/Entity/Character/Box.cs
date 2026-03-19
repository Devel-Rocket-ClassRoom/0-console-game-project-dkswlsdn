using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Box : CharacterEntity
{
    public Box(Scene scene, Point point) : base(scene, point)
    {
        Health = 100;

        RectAngle = new RectAngle(this, ((-2, 0), (2, 10)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);
        buffer.WriteText(Position.WinXY.X, Position.WinXY.Y, Health.ToString());
    }

    public override void TakeDamage(int attackId, int damage, int immuneDuration)
    {
        long currentTime = Environment.TickCount64;

        if (ImmunityList.TryGetValue(attackId, out long endTime))
        {
            if (currentTime < endTime)
            {
                return;
            }

            ImmunityList.Remove(attackId);
        }

        Health -= damage;
        ImmunityList[attackId] = currentTime + immuneDuration;
    }

    public override void Update(float deltaTime)
    {
        RectAngle.Follow();
    }
}