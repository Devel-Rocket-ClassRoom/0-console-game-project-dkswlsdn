using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Box : CharacterEntity
{
    int a = 0;
    private float _attackCooldown = 1f;
    private float _currentCooldown = 0f;

    public Box(Scene scene, Point point) : base(scene, point)
    {
        Health = 100;

        RectAngle = new RectAngle(this, (5, 11));

        _currentPixels = _idlePixels;
    }

    public Point Aim { get; set; } = (-1, 0);

    public void Aimming()
    {
        Aim = (-1, 0);
    }

    public void Attack(float deltaTime)
    {
        if (_currentCooldown <= 0 && !_isDead)
        {
            new HandgunBullet(Scene, this, (Position.X, Position.Y), Aim, true);
            _currentCooldown = _attackCooldown;
        }
        else
        {
            _currentCooldown -= deltaTime;
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Position - (0, 1), Health.ToString());
        buffer.WriteText(Position, _currentCooldown.ToString());
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        Attack(deltaTime);
        Aimming();
    }

    public override void DeadMotion(float deltaTime)
    {
        _currentPixels = _deadPixels;
    }


    private string[] _idlePixels =
    {
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
        "DDDDD",
    };

    private string[] _deadPixels =
    {
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
        "GGGGG",
    };
}