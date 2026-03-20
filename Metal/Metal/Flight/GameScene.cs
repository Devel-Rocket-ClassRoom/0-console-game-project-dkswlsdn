using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GameScene : Scene
{
    public event GameAction ToTitleRequest;

    public List<CharacterEntity> EntityList = new List<CharacterEntity>();
    public List<GroundEntity> GroundEntitiyList = new List<GroundEntity>();

    Player player;
    Camera camera;
    Ground ground;
    Ground ground2;
    Ground ground3;
    DamagableEntity entity;
    Box box;
    

    public override void Load()
    {
        player = new Player(this, (30, 30));
        camera = new Camera(this, player);
        ground = new Ground(this, (0, 5), 800);
        ground2 = new Ground(this, (40, 25), 40);
        ground3 = new Ground(this, (120, 40), 80);
        box = new Box(this, (100, 41));

        AddGameObject(ground);
        AddGameObject(ground2);
        AddGameObject(ground3);
        AddGameObject(player);
        AddGameObject(camera);
        AddGameObject(box);
    }

    public override void Unload()
    {
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.FillRect(0, 0, ShottingGame.k_Width, ShottingGame.k_Height, bgColor:ConsoleColor.DarkCyan);
        buffer.WriteText((1, 1), $"GameScene : count: {EntityList.Count}", bgColor:ConsoleColor.DarkCyan);
        buffer.WriteText((1, 7), ground.Position.ToString());
        buffer.WriteText((1, 8), ground2.Position.ToString());

        DrawGameObjects(buffer);
    }

    

    public void Error()
    {
        ToTitleRequest?.Invoke();
    }
}