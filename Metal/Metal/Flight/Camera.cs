using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : GameObject
{
    private Point _adjustment;

    public int LeftClamp = 0;
    public int RightClamp = 100;


    public static Point Position = (0, 0);
    public bool FollowPlayer = true;
    public Point Adjustment { get { return _adjustment; } set { _adjustment = (ShottingGame.k_Width / 2 / value.X, ShottingGame.k_Height / 2 / value.Y); } }
    private CharacterEntity _player;

    public Camera(Scene scene, CharacterEntity player) : base(scene)
    {
        _player = player;
        Adjustment = (4, 5);
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
        if (FollowPlayer)
        {
            Position.X = Math.Clamp(_player.Position.X - Adjustment.X, 0, RightClamp);
        }
    }
}