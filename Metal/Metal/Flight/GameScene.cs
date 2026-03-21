using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class GameScene : Scene
{
    public event GameAction ToTitleRequest;

    public List<CharacterEntity> EntityList = new List<CharacterEntity>();
    public List<Ground> GroundEntitiyList = new List<Ground>();
    public List<Wall> WallEntitiyList = new List<Wall>();

    Player player;
    Camera camera;
    Ground ground;
    Ground ground2;
    Ground ground3;
    Ground ground4;
    Wall wall;
    DamagableEntity entity;
    Box box;
    GetWeapon GetWeaponTemp;
    GetWeapon GetWeaponTemp2;
    

    public override void Load()
    {
        player = new Player(this, (30, 30));
        camera = new Camera(this, player);
        ground = new Ground(this, (0, 5), 800, "ground");
        ground2 = new Ground(this, (40, 25), 40, "platform");
        ground3 = new Ground(this, (120, 40), 80, "higherPlatform");
        box = new Box(this, (100, 45));
        GetWeaponTemp = new GetShotgun(this, (150, 20));
        GetWeaponTemp2 = new GetHeavyMachinegun(this, (200, 20));
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
        buffer.WriteText(Camera.Position + (0, 30), _gameObjects.Count.ToString());

        DrawGameObjects(buffer);
    }

    

    public void Error()
    {
        ToTitleRequest?.Invoke();
    }
}