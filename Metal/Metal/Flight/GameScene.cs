using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GameScene : Scene
{
    static int nextId = 0;

    public event GameAction ToTitleRequest;

    public List<CharacterEntity> EntityList = new List<CharacterEntity>();

    Player player;
    Ground ground;
    DamagableEntity entity;

    public override void Load()
    {
        player = new Player(this, nextId++);
        ground = new Ground(this);
        entity = new DamagableEntity(this, nextId++, 10);

        AddGameObject(player);
        AddGameObject(player.RectAngle);
        AddGameObject(ground);
        AddGameObject(entity);
        AddGameObject(entity.RectAngle);
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
        buffer.FillRect(0, 0, ShottingGame.k_Width, ShottingGame.k_Height, bgColor:ConsoleColor.DarkCyan);
        buffer.WriteText(1, 1, "GameScene", bgColor:ConsoleColor.DarkCyan);

        DrawGameObjects(buffer);
    }

    private bool IsOnGround()
    {
        if (player.IsOnGround)
        {
            return true;
        }

        bool result = false;

        for (int i = 0; i < -player.JumpForce; i++)
        {
            if (ground.GroundPosition.Contains(player.GetNextPosition(i)))
            {
                result = true;
                player.JumpCooldown = 0.05f;
                break;
            }
        }

        return result;
    }

    public void Error()
    {
        ToTitleRequest?.Invoke();
    }
}