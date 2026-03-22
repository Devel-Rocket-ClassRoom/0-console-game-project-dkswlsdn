using Framework.Engine;


public class GranadeBullet : BulletEntity
{
    private bool _alreadyBounce = false;

    public GranadeBullet(Scene scene, Point point, Point aim) : base(scene, point, (1, 0), 5, 5)
    {
        Type = EntityType.Bullet;
        Mask = EntityType.Ground | EntityType.Enemy;

        _useGravity = true;

        _life = 10f;
        _bulletSpeed = 80f;
        _gravity = 240f;
        Damage = 100;

        _isOnlyTarget = true;
        _interval = 0.2f;

        _currentPixels = _idelPixels;

        Velocity = aim.Normalize() * _bulletSpeed;
    }


    public override void CollisionToStatic()
    {
        if (!_alreadyBounce)
        {
            _alreadyBounce = true;
            Velocity += (0, 40);
        }
        else
        {
            Scene.AddGameObject(new GranadeSplash(Scene, Position));
            Granade.granadeCount--;
            Destroy();
        }
    }
    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
        Scene.AddGameObject(new GranadeSplash(Scene, Position));
        Granade.granadeCount--;
        Destroy();
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