using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class Player : CharacterEntity, IMoveable, IJumpable
{
    private bool _isLand = false;
    private int _aim = 1;
    private int _jumpForce = 0;
    private Point _direction;

    public float JumpCooldown { private get;  set; } = 0.1f;


    public Point NextPosition { get { return (Position.X, Position.Y - 6); } }
    public Point GroundChecker { get { return Position - (0, 1); } }
    public int JumpForce { get { if (_jumpForce < 0 && _isLand) _jumpForce = 0; return _jumpForce; } set { _jumpForce = value; } }
    public int Health { get; private set; }
    public Dictionary<int, long> ImmunityList { get; set; }



    private string[] pixels = // C = Cyan, D = DarkBlue, B = Black, G = DarkGray
    {
        " DDD ",
        " DCC ",
        " DCC ",
        "BBBBB",
        "BGGGB",
        "BGGGB",
        "DBBBD",
        " B B ",
        " B B ",
        " B B ",
        " D D "
    };

    public Player(Scene scene, Point point) : base(scene, point)
    {
        _direction = (1, 0);

        Health = 100;

        RectAngle = new RectAngle( this, ((-2, 0), (2, 10)));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawPlayer(buffer);

        buffer.WriteText(1, 2, $"{Position.X}, {Position.Y}");
        buffer.WriteText(1, 3, $"HP : {Health}");
        buffer.WriteText(1, 3, $"Jump : {JumpForce}");
        //buffer.WriteText(1, 4, $"IsOnGround : {IsOnGround}");
        buffer.WriteText(1, 5, $"isLand : {_isLand}");
    }

    public override void Update(float deltaTime)
    {
        if (_isLand)
        {
            JumpForce = 0;
            Jump(deltaTime);
        }
        else
        {
            Land();
        }

        Move(2);
    }

    public void Move(int speed)
    {
        if (Input.IsKey(ConsoleKey.D))
        {
            _aim = 1;

            if (_isLand)
                _direction = (1, 0);
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _aim = -1;

            if (_isLand)
                _direction = (-1, 0);
        }
        else
        {
            if (_isLand)
                _direction = (0, 0);
        }
        
        Position += (speed * _direction.X, 0);
    }

    public void VirticalMove(int force)
    {
        Position += (0, force);
    }

    public void Jump(float deltaTime)
    {
        if (JumpCooldown > 0)
        {
            JumpCooldown -= deltaTime;
            return;
        }

        if (Input.IsKey(ConsoleKey.Spacebar))
        {
            JumpForce = 7;
            _isLand = false;
        }
    }

    public void Land()
    {
        if (Scene is GameScene g)
        {
            for (int i = 0; i < g.GroundEntitiyList.Count; i++)
            {
                if (JumpForce < 0 && g.GroundEntitiyList[i].RectAngle.IsOverrap((Position.X, Position.Y + JumpForce), Position))
                {
                    JumpCooldown = 0.1f;
                    _isLand = true;
                    JumpForce = 0;
                    Position.Y = g.GroundEntitiyList[i].Position.Y + 1;
                    return;
                }
            }
        }

        VirticalMove(JumpForce--);
    }

    public void DrawPlayer(ScreenBuffer buffer)
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                ConsoleColor color = ConsoleColor.Black;

                switch (pixels[j][i * _aim + 2])
                {
                    case 'C':
                        color = ConsoleColor.Cyan;
                        break;
                    case 'D':
                        color = ConsoleColor.DarkBlue;
                        break;
                    case 'B':
                        color = ConsoleColor.Black;
                        break;
                    case 'G':
                        color = ConsoleColor.DarkGray;
                        break;
                    default:
                        continue;
                }

                buffer.SetCell((Position + new Point(i, -j + 10)).WinXY, color);
            }
        }

        /*
        //머리
        buffer.SetCell(Position + (0, -8), ConsoleColor.Cyan);
        buffer.SetCell(Position + (0, -9), ConsoleColor.Cyan);

        buffer.SetCell(Position + (_aim, -8), ConsoleColor.Cyan);
        buffer.SetCell(Position + (_aim, -9), ConsoleColor.Cyan);
        buffer.SetCell(Position + (-_aim, -8), ConsoleColor.DarkBlue);
        buffer.SetCell(Position + (-_aim, -9), ConsoleColor.DarkBlue);
        
        buffer.SetCell(Position + (-1, -10), ConsoleColor.DarkBlue);
        buffer.SetCell(Position + (0, -10), ConsoleColor.DarkBlue);
        buffer.SetCell(Position + (1, -10), ConsoleColor.DarkBlue);

        //팔, 어깨
        buffer.SetCell(Position + (2, -5), ConsoleColor.Black);
        buffer.SetCell(Position + (-2, -5), ConsoleColor.Black);
        buffer.SetCell(Position + (2, -6), ConsoleColor.Black);
        buffer.SetCell(Position + (-2, -6), ConsoleColor.Black);
        buffer.SetCell(Position + (-2, -7), ConsoleColor.Black);
        buffer.SetCell(Position + (-1, -7), ConsoleColor.Black);
        buffer.SetCell(Position + (0, -7), ConsoleColor.Black);
        buffer.SetCell(Position + (1, -7), ConsoleColor.Black);
        buffer.SetCell(Position + (2, -7), ConsoleColor.Black);

        //손
        buffer.SetCell(Position + (2, -4), ConsoleColor.DarkBlue);
        buffer.SetCell(Position + (-2, -4), ConsoleColor.DarkBlue);

        //몸통
        buffer.SetCell(Position + (-1, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(Position + (0, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(Position + (1, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(Position + (-1, -6), 'B', 'R', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(Position + (0, -6), 'U', 'T', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(Position + (1, -6), 'A', 'L', ConsoleColor.White, ConsoleColor.DarkGray);

        //다리
        buffer.SetCell(Position + (1, -1), ConsoleColor.Black);
        buffer.SetCell(Position + (-1, -1), ConsoleColor.Black);
        buffer.SetCell(Position + (1, -2), ConsoleColor.Black);
        buffer.SetCell(Position + (-1, -2), ConsoleColor.Black);
        buffer.SetCell(Position + (1, -3), ConsoleColor.Black);
        buffer.SetCell(Position + (-1, -3), ConsoleColor.Black);
        buffer.SetCell(Position + (1, -4), ConsoleColor.Black);
        buffer.SetCell(Position + (-1, -4), ConsoleColor.Black);
        buffer.SetCell(Position + (0, -4), ConsoleColor.Black);

        //발
        buffer.SetCell(Position + (1, 0), ConsoleColor.DarkBlue);
        buffer.SetCell(Position + (-1, 0), ConsoleColor.DarkBlue);
        */
    }

    public override void TakeDamage(int attackId, int damage, int immuneDuration = 100)
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
        ImmunityList[attackId] =  currentTime + immuneDuration;
    }


    public Point GetNextPosition(int lowerForce)
    {
        throw new NotImplementedException();
    }
}