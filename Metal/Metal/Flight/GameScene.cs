using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GameScene : Scene
{
    Player player;
    Ground ground;

    public override void Load()
    {
        player = new Player(this);
        ground = new Ground(this);
        AddGameObject(player);
        AddGameObject(ground);
    }

    public override void Unload()
    {
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);

        player.IsOnGround = IsOnGround();
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(0, 0, 160, 40);
        buffer.WriteText(1, 1, "GameScene");

        DrawGameObjects(buffer);
    }

    private bool IsOnGround()
    {
        return true;
        //return ground.GroundPosition.Contains(player.NextPosition);
    }
}