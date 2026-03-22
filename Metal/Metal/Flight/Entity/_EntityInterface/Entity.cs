using Framework.Engine;
using System.Drawing;
using System.Security.Cryptography;

public abstract class Entity : GameObject
{
    public static int nextId = 1;
    public int ID { get; private set; }

    public EntityType Type;
    public EntityType Mask;
    public float Threshold { get { return Math.Max(Width, Height); } }


    public int Width;
    public int Height;

    public float Right { get { return Position.X + Width - 1; } }
    public float Left { get { return Position.X; } }
    public float Top { get { return Position.Y + Height - 1; } }
    public float Bottom { get { return Position.Y; } }
    public Point Center { get { return (Position.X + Width / 2, Position.Y + Height / 2); } }
    public (Point a, Point b) Foot { get { return ((Left, Bottom - 1f), (Right, Bottom)); } }


    protected bool _canMove;
    protected bool _useGravity;
    protected Point _currentVelocity;
    protected float _gravity = 180;
    public Point Velocity
    {
        get { return _currentVelocity; }
        set
        {
            _currentVelocity = value;
        }
    }
    protected bool _isLand;



    public Entity(Scene scene, Point position, bool isDynamic)
        : base(scene, position)
    {
        ID = nextId++;
        Width = 1;
        Height = 1;

        //if (isTrigger) scene.TriggerEntityList.Add(this);
        if (isDynamic) scene.DynamicEntityList.Add(this);
        else scene.StaticEntityList.Add(this);
    }


    public override void Update(float deltaTime)
    {
        if (_canMove || _useGravity) Move(deltaTime);
        if (_useGravity) { VirticalMove(deltaTime); }
    }

    protected virtual void Move(float deltaTime)
    {
        float moveX = Velocity.X * deltaTime;
        float moveY = Velocity.Y * deltaTime;

        int stepsX = (int)Math.Abs(moveX) + 1;
        float stepX = moveX / stepsX;

        for (int i = 0; i < stepsX; i++)
        {
            Position = new Point(Position.X + stepX, Position.Y);
            if (Physics.IsOverrap(this, Scene.StaticEntityList, out _))
            {
                Position = new Point(Position.X - stepX, Position.Y);
                Velocity = new Point(0, Velocity.Y);
                CollisionToStatic();
                break;
            }
        }

        int stepsY = (int)Math.Abs(moveY) + 1;
        float stepY = moveY / stepsY;

        for (int i = 0; i < stepsY; i++)
        {
            Position = new Point(Position.X, Position.Y + stepY);
            if (Physics.IsOverrap(this, Scene.StaticEntityList, out _))
            {
                Position = new Point(Position.X, Position.Y - stepY);
                Velocity = new Point(Velocity.X, 0);
                _isLand = true;
                CollisionToStatic();
                break;
            }
        }
    }

    protected virtual void VirticalMove(float deltaTime)
    {
        CheckGround();
        if (!_isLand) { Velocity -= (0, _gravity * deltaTime); }
    }


    public virtual void CollisionToStatic() { }
    public virtual void CollisionFromDynamic(int attackid = 0, int damage = 0) { }

    



    protected virtual void CheckCameraBounds(bool immediatelyDelelte)
    {
        float screenW = ShottingGame.k_Width / 2;
        float screenH = ShottingGame.k_Height;
        float margin = 30;

        float leftBound = immediatelyDelelte ? Camera.Position.X - Width : Camera.LeftClamp - margin;
        float rightBound = immediatelyDelelte ? Camera.Position.X + screenW: Camera.RightClamp + screenW + margin;
        float bottomBound = Camera.Position.Y - margin;
        float topBound = Camera.Position.Y + screenH + margin;

        if (Position.X < leftBound || Position.X > rightBound ||
            Position.Y < bottomBound || Position.Y > topBound)
        {
            Destroy();
        }
    }



    protected virtual void CheckGround()
    {
        float checkY = Bottom - 3;

        _isLand = false;

        foreach (var ground in Scene.StaticEntityList)
        {
            if (ground is GroundEntity || ground is PlatformEntity)
            {
                if (checkY <= ground.Top && Bottom >= ground.Top)
                {
                    if (Right >= ground.Left && Left <= ground.Right)
                    {
                        _isLand = true;
                        CollisionToStatic();

                        if (Velocity.Y < 0)
                            Velocity = new Point(Velocity.X, 0);

                        break;
                    }
                }
            }
        }
    }


    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
        buffer.DrawBox(Position + (0, Height), Width, Height, bgColor: ConsoleColor.Red);
    }


    [Flags]
    public enum EntityType
    {
        None = 0,
        Ground = 1,
        Player = 1 << 1,
        Enemy = 1 << 2,
        Bullet = 1 << 3,
        Trigger = 1 << 4,
        Platform = 1 << 5,
    }
}

