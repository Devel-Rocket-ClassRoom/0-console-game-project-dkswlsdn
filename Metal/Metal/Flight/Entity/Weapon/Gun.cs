using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Gun : GameObject
{
    public Point Position;
    private Bullet _bullet;
    private int _arms;

    public Gun(Scene scene) : base(scene)
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        throw new NotImplementedException();
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKey(ConsoleKey.LeftArrow))
        {
            //Scene.AddGameObject(new Bullet(Scene, Position, 1, ));
        }
    }
}