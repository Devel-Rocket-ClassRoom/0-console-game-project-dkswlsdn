using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class Player : Entity, IMoveable, IJumpable
{
    private int _aim = 1;
    private Point _direction;

    public float JumpCooldown { private get;  set; } = 0.1f;


    public Point NextPosition { get { return (_position.x, _position.y - JumpForce); } }
    public bool IsOnGround { get; set; } = false;
    public int JumpForce { get; private set; } = 0;



    public Player(Scene scene) : base(scene)
    {
        _position = (10, 37);
        _direction = (1, 0);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawPlayer(buffer);

        buffer.WriteText(1, 2, $"{_position.x}, {_position.y}");
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
        
        _position += (speed * _direction.x, 0);
    }

    public void VirticalMove(int force)
    {
        if (!IsOnGround)
        {
            _position += (0, -force);
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
        buffer.SetCell(_position + (0, -8), ConsoleColor.Cyan);
        buffer.SetCell(_position + (0, -9), ConsoleColor.Cyan);

        buffer.SetCell(_position + (_aim, -8), ConsoleColor.Cyan);
        buffer.SetCell(_position + (_aim, -9), ConsoleColor.Cyan);
        buffer.SetCell(_position + (-_aim, -8), ConsoleColor.DarkBlue);
        buffer.SetCell(_position + (-_aim, -9), ConsoleColor.DarkBlue);
        
        buffer.SetCell(_position + (-1, -10), ConsoleColor.DarkBlue);
        buffer.SetCell(_position + (0, -10), ConsoleColor.DarkBlue);
        buffer.SetCell(_position + (1, -10), ConsoleColor.DarkBlue);

        //팔, 어깨
        buffer.SetCell(_position + (2, -5), ConsoleColor.Black);
        buffer.SetCell(_position + (-2, -5), ConsoleColor.Black);
        buffer.SetCell(_position + (2, -6), ConsoleColor.Black);
        buffer.SetCell(_position + (-2, -6), ConsoleColor.Black);
        buffer.SetCell(_position + (-2, -7), ConsoleColor.Black);
        buffer.SetCell(_position + (-1, -7), ConsoleColor.Black);
        buffer.SetCell(_position + (0, -7), ConsoleColor.Black);
        buffer.SetCell(_position + (1, -7), ConsoleColor.Black);
        buffer.SetCell(_position + (2, -7), ConsoleColor.Black);

        //손
        buffer.SetCell(_position + (2, -4), ConsoleColor.DarkBlue);
        buffer.SetCell(_position + (-2, -4), ConsoleColor.DarkBlue);

        //몸통
        buffer.SetCell(_position + (-1, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(_position + (0, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(_position + (1, -5), ' ', ' ', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(_position + (-1, -6), 'B', 'R', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(_position + (0, -6), 'U', 'T', ConsoleColor.White, ConsoleColor.DarkGray);
        buffer.SetCell(_position + (1, -6), 'A', 'L', ConsoleColor.White, ConsoleColor.DarkGray);

        //다리
        buffer.SetCell(_position + (1, -1), ConsoleColor.Black);
        buffer.SetCell(_position + (-1, -1), ConsoleColor.Black);
        buffer.SetCell(_position + (1, -2), ConsoleColor.Black);
        buffer.SetCell(_position + (-1, -2), ConsoleColor.Black);
        buffer.SetCell(_position + (1, -3), ConsoleColor.Black);
        buffer.SetCell(_position + (-1, -3), ConsoleColor.Black);
        buffer.SetCell(_position + (1, -4), ConsoleColor.Black);
        buffer.SetCell(_position + (-1, -4), ConsoleColor.Black);
        buffer.SetCell(_position + (0, -4), ConsoleColor.Black);

        //발
        buffer.SetCell(_position + (1, 0), ConsoleColor.DarkBlue);
        buffer.SetCell(_position + (-1, 0), ConsoleColor.DarkBlue);
    }

    public Point GetNextPosition(int lowerForce)
    {
        return (_position.x, _position.y + lowerForce);
    }
}