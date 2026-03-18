using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class Player : Entity, IMoveable, IJumpable, IHealthHaveable
{
    private int _aim = 1;
    private Point _direction;

    public float JumpCooldown { private get;  set; } = 0.1f;


    public (Point, Point) RectAngle { get; }
    public Point NextPosition { get { return (Position.x, Position.y - JumpForce); } }
    public bool IsOnGround { get; set; } = false;
    public int JumpForce { get; private set; } = 0;
    public int Health { get; private set; }
    public Dictionary<int, long> ImmunityList { get; set; }




    public Player(Scene scene) : base(scene)
    {
        Position = (10, 37);
        _direction = (1, 0);

        Health = 100;

        RectAngle = (Position + (-2, -10), Position + (2, 0));
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawPlayer(buffer);

        buffer.WriteText(1, 2, $"{Position.x}, {Position.y}");
        buffer.WriteText(1, 3, $"HP : {Health}");
    }

    public override void Update(float deltaTime)
    {
        Jump(deltaTime);

        Move(2);

        VirticalMove(JumpForce--);
        
    }

    public void Move(int speed)
    {
        if (Input.IsKey(ConsoleKey.D))
        {
            _aim = 1;

            if (IsOnGround)
                _direction = (1, 0);
        }
        else if (Input.IsKey(ConsoleKey.A))
        {
            _aim = -1;

            if (IsOnGround)
                _direction = (-1, 0);
        }
        else
        {
            if (IsOnGround)
                _direction = (0, 0);
        }
        
        Position += (speed * _direction.x, 0);
    }

    public void VirticalMove(int force)
    {
        if (!IsOnGround)
        {
            Position += (0, -force);
        }
    }

    public void Jump(float deltaTime)
    {
        if (JumpCooldown > 0)
        {
            JumpCooldown -= deltaTime;
            return;
        }

        if (Input.IsKey(ConsoleKey.Spacebar) && IsOnGround)
        {
            JumpForce = 7;
            IsOnGround = false;
        }
    }

    public void DrawPlayer(ScreenBuffer buffer)
    {
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
    }

    public Point GetNextPosition(int lowerForce)
    {
        return (Position.x, Position.y + lowerForce);
    }

    public void TakeDamage(int attackId, int damage, int immuneDuration = 100)
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
}