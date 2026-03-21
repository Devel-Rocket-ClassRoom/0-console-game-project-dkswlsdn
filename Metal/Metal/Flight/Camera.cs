using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : Entity
{
    private Point _adjustment;

    public int LeftClamp = 0;
    public int RightClamp = 200;

    private Wall _leftClampWall;
    private Wall _rightClampWall;


    public static Point Position = (0, 0);
    public bool FollowPlayer = true;
    public Point Adjustment { get { return _adjustment; } set { _adjustment = (ShottingGame.k_Width / 2 / value.X, ShottingGame.k_Height / 2 / value.Y); } }
    private CharacterEntity _player;

    public Camera(Scene scene, CharacterEntity player) : base(scene, (0, 0))
    {
        _player = player;
        Adjustment = (4, 5);

        _leftClampWall = new Wall(Scene, (LeftClamp - 1, 50), 100, "LeftClamp");
        _rightClampWall = new Wall(Scene, (RightClamp + ShottingGame.k_Width / 2, 50), 100, "RightClamp");
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

        _leftClampWall.Position = (LeftClamp - 1, 50);
        _rightClampWall.Position = (RightClamp + ShottingGame.k_Width / 2, 50);
    }
}