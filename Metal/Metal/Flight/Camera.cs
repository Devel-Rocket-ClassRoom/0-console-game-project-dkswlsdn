using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : GameObject
{
    private Point _adjustment;

    private static int _leftClamp = 0;
    private static int _rightClamp = 300;

    public static int LeftClamp { get { return _leftClamp; } set { if (!LockLeftClamp && value > _leftClamp) _leftClamp = value; } }
    public static int RightClamp { get { return _rightClamp; } set { if (!LockRightClamp) _rightClamp = value; } }
    public static bool LockLeftClamp = false;
    public static bool LockRightClamp = false;


    public new static Point Position = (0, 0);
    public Player player;
    public Point Adjustment { get { return _adjustment; } set { _adjustment = (ShottingGame.k_Width / 2 / value.X, ShottingGame.k_Height / 2 / value.Y); } }

    public Camera(GameScene scene) : base(scene, (0, 0))
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
        if (Scene is GameScene game && game.player != null)
        {
            float targetX = Math.Clamp(player.Position.X - ShottingGame.k_Width / 4, LeftClamp, RightClamp);
            float targetY = Math.Max(player.Position.Y - ShottingGame.k_Height / 1.3f, 0);

            float lerpFactor = 5.0f * deltaTime;
            if (lerpFactor > 1.0f) lerpFactor = 1.0f;

            float nextX = Position.X + (targetX - Position.X) * lerpFactor;
            float nextY = Position.Y + (targetY - Position.Y) * lerpFactor;

            Position = (nextX, nextY);

            LeftClamp = (int)Position.X;
        }
    }
}