using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class ShotgunBullet : BulletEntity
{
    public ShotgunBullet(GameScene scene, Point point, Point aim)
        : base(scene, point, aim, 46, 17)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Enemy;

        
        _canMove = false;
        _useGravity = false;

        _bulletSpeed = 0;
        _life = 0.2f;
        Damage = 200;

        _interval = 0.3f;
        _isOnlyTarget = false;

        _currentPixels = _idelPixels;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position, Position.ToString());
    }

    protected override void CheckDynamicCollision()
    {
        for (int i = 0; i < Scene.DynamicEntityList.Count; i++)
        {
            if (Physics.IsOverrap(this, Scene.DynamicEntityList[i]))
            {
                Scene.DynamicEntityList[i].CollisionFromDynamic(ID, Damage);
                CollisionFromDynamic();
            }
        }
    }



    public string[] _idelPixels =
    {
        "                               WWWW          ",
        "                          WWWWWWWWWWWW       ",
        "                     WWWWWWWWWWWWWWWWWWWW    ",
        "                WWWWWWWWWWWWWWWWWWWWWWWWWWW  ",
        "            WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW  ",
        "         WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW ",
        "      WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
        "   WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
        "   WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
        "      WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
        "         WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW ",
        "            WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW  ",
        "                WWWWWWWWWWWWWWWWWWWWWWWWWWW  ",
        "                     WWWWWWWWWWWWWWWWWWWW    ",
        "                          WWWWWWWWWWWW       ",
        "                               WWWW          ",
    };
}
