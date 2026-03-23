using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class GameScene : Scene
{
    public Player player;
    public Camera camera;


    public override void Update(float deltaTime)
    {
        if (player == null)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter)) { NewCoinInsert(); }
        }
        else if (!player.IsAlive)
        {
            player = null;
        }
    }

    public override void Load()
    {
        camera = new Camera(this);

        AddGameObject(camera);
    }

    public void NewCoinInsert()
    {
        player = new Player(this, Camera.Position + (20, 80));
        camera.player = player;
        AddGameObject(player);
    }
}