using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class Player : Entity, IMoveable, IJumpable
{
    private (int x, int y) _direction;


    public (int x, int y) NextPosition { get { return (_position.x, _position.y - 1); } }
    public bool IsOnGround { get; set; }
    public int JumpForce { get; private set; } = 6;
    public float BlockPerSecond { get; }



    public Player(Scene scene) : base(scene)
    {
        _position = (10, 30);
        _direction = (1, 0);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(_position.x, _position.y - 1, "o");

        if (_direction.x == 1)
        {
            buffer.WriteText(_position.x, _position.y, "|-");
        }
        else if (_direction.x == -1)
        {
            buffer.WriteText(_position.x - 1, _position.y, "-|");
        }

        buffer.WriteText(_position.x, _position.y + 1, "|");



        buffer.WriteText(1, 2, $"{_position.x}, {_position.y}");
    }

    public override void Update(float deltaTime)
    {
        Move();
        Jump();
    }

    public void Move()
    {
        if (IsOnGround)
        {
            if (Input.IsKey(ConsoleKey.D))
            {
                _direction = (1, 0);
                _position = (_position.x + 1, _position.y);
            }
            else if (Input.IsKey(ConsoleKey.A))
            {
                _direction = (-1, 0);
                _position = (_position.x - 1, _position.y);
            }
        }
    }

    public void Jump()
    {
        if (Input.IsKeyDown(ConsoleKey.Spacebar))
        {
            IsOnGround = false;
        }


    }
}