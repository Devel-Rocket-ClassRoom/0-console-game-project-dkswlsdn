using Framework.Engine;
using System.Drawing;

public abstract class Entity : GameObject
{
    public string Name { get; set; }
    public static int nextId = 1;
    public int ID { get; private set; }


    protected string[] _currentPixels;
    protected bool CurrentIsRight { get { return Direction.X == 1; } }

    protected (float X, float Y) _realPosition = (0, 0);
    public Point Position
    {
        get { return (_realPosition.X, _realPosition.Y); }
        set { _realPosition = (value.X, value.Y); }
    }
    public Point Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
        }
    }


    private (int X, int Y) _direction = (1, 0);
    public RectAngle RectAngle { get; protected set; }


    protected bool _pixelReversed;


    public Entity(Scene scene, Point point, bool instant = true) : base(scene)
    {
        Position = new Point(point);
        ID = nextId++;

        if (instant) Scene.AddGameObject(this);
    }




    public virtual void DrawEntity(ScreenBuffer buffer)
    {
        int width = _currentPixels[0].Length;
        int height = _currentPixels.Length;

        int midX = width / 2;
        int midy = height / 2;

        int n = _pixelReversed ? -1 : 1;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                char pixel = _currentPixels[j][i];
                if (pixel == ' ' || pixel == '\0') continue;

                ConsoleColor color = GetColor(pixel);

                int relX = i - midX;
                int relY = -j + midy;

                int drawX = 0;
                int drawY = 0;

                switch ((Direction.X, Direction.Y))
                {
                    case (1, 0):
                        drawX = relX;
                        drawY = relY * n;
                        break;

                    case (-1, 0):
                        drawX = -relX;
                        drawY = relY * n;
                        break;

                    case (0, 1):
                        drawX = relY * n;
                        drawY = relX;
                        break;

                    case (0, -1):
                        drawX = relY * n;
                        drawY = -relX;
                        break;
                }

                buffer.SetCell(Position + new Point(drawX, drawY), color);
            }
        }
    }

    public ConsoleColor GetColor(char c)
    {
        switch (c)
        {
            case 'C':
                return ConsoleColor.Cyan;
            case 'D':
                return ConsoleColor.DarkBlue;
            case 'B':
                return ConsoleColor.Black;
            case 'G':
                return ConsoleColor.DarkGray;
            case 'R':
                return ConsoleColor.Red;
            case 'Y':
                return ConsoleColor.DarkYellow;
            case 'y':
                return ConsoleColor.Yellow;
            case 'g':
                return ConsoleColor.DarkGreen;
            case 'W':
            default:
                return ConsoleColor.White;
        }
    }


    public override void Update(float deltaTime)
    {
        if (RectAngle != null)
        {
            RectAngle.SpinRect(Direction);
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawEntity(buffer);
        //if (RectAngle != null) RectAngle.DrawRectAngle(buffer);
        //buffer.SetCell(Position + (0, 1), ConsoleColor.Green);
    }



    public enum Motion
    {
        Idle, Walk, WalkLookUp, Lookup, Sit, Crawl, Jump, JumpLookUp, LookDown,
    }
}