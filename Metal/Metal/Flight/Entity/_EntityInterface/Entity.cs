using Framework.Engine;
using System.Drawing;

public abstract class Entity : GameObject
{
    public static int nextId = 1;
    public int ID { get; private set; }


    protected string[] _currentPixels;
    protected bool _currentIsRight = true;
    public Point Position;
    public int Direction
    {
        get { return _direction; }
        set
        {
            if (value < 0 || value >= 4)
            {
                value = 0;
            }

            _direction = value;
        }
    }
    private int _direction = 0;
    protected Point _runningDirection;
    public RectAngle RectAngle { get; protected set; }


    public Entity(Scene scene, Point point) : base(scene)
    {
        Position = new Point(point);
        ID = nextId++;
        _runningDirection = (0, 0);
    }

    public virtual void DrawEntity(ScreenBuffer buffer)
    {
        int width = _currentPixels[0].Length;
        int height = _currentPixels.Length;

        if (_runningDirection.X != 0) _currentIsRight = _runningDirection.X == 1;
        int n = _currentIsRight ? 1 : -1;

        int midX = width / 2;
        int bottomY = height - 1;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                char pixel = _currentPixels[j][i];
                if (pixel == ' ' || pixel == '\0') continue;

                ConsoleColor color = GetColor(pixel);
                int relX = i - midX;
                int relY = bottomY - j;

                int drawX = 0;
                int drawY = 0;

                switch (Direction)
                {
                    case 0:
                        drawX = relX * n;
                        drawY = relY;
                        break;

                    case 1:
                        drawX = relY;
                        drawY = relX * n;
                        break;

                    case 2:
                        drawX = relX * n;
                        drawY = -relY;
                        break;

                    case 3:
                        drawX = -relY;
                        drawY = relX * n;
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
            case 'A':
                return ConsoleColor.DarkGreen;
            default:
                return ConsoleColor.White;
        }
    }


    public override void Update(float deltaTime)
    {
        RectAngle.SpinRect(Direction, _currentIsRight);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawEntity(buffer);
        RectAngle.DrawRectAngle(buffer);
        //buffer.WriteText(Position + (0, 1), Direction.ToString());
    }
}