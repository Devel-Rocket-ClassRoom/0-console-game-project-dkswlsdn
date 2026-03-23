using Framework.Engine;


public class GranadeBullet : BulletEntity
{
    private bool _alreadyBounce = false;
    private float _explosiveLatency = 0.4f;

    public GranadeBullet(GameScene scene, Point point, Point aim) : base(scene, point, (1, 0), 5, 5)
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
            Velocity = (Velocity.X, 50);
        }
        else if (_explosiveLatency <= 0)
        {
            Scene.AddGameObject(new GranadeSplash_2(Scene, Position));
            Scene.AddGameObject(new GranadeSplash(Scene, Position));
            //Granade.granadeCount--;
            Destroy();
        }
    }
    public override void CollisionFromDynamic(int id = 0, int damage = 0)
    {
        Scene.AddGameObject(new GranadeSplash_2(Scene, Position));
        Scene.AddGameObject(new GranadeSplash(Scene, Position));
        //Granade.granadeCount--;
        Destroy();
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (_alreadyBounce)
        {
            _explosiveLatency -= deltaTime;
        }
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