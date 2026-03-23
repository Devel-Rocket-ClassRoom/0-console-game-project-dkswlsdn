using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class LazorBullet : BulletEntity
{
    Boss _boss;
    Point _position;

    public LazorBullet(GameScene scene, Point point, Boss boss) : base(scene, point, (1, 0), 10, 100)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Player;

        _canMove = false;
        _useGravity = false;

        _bulletSpeed = 0;
        _life = 4f;
        Damage = 1;

        _isOnlyTarget = false;
        _boss = boss;
        _position = point;

        _currentPixels = _idelPixels;
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

    public override void Update(float deltaTime)
    {
        Position = _boss.Position + _position;

        base.Update(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Camera.Position + (0, 10), Position.ToString());
    }

    public string[] _idelPixels =
    {
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
        "WWWWWWWWWW",
    };
}