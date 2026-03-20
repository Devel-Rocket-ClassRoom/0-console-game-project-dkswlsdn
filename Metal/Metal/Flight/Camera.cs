using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Camera : GameObject
{
    public static Point Position = (0, 0);
    public Point Adjustment = (0, 0);
    private CharacterEntity _player;

    public Camera(Scene scene, CharacterEntity player) : base(scene)
    {
        _player = player;
        Adjustment = (ShottingGame.k_Width / 10, ShottingGame.k_Height / 10);
    }

    public override void Draw(ScreenBuffer buffer)
    {
    }

    public override void Update(float deltaTime)
    {
        Position = (_player.Position.X, 0) - Adjustment;
    }
}