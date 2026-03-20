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
    Ground ground4;
    DamagableEntity entity;
    Box box;
    GetShotgun GetWeaponTemp;
    GetShotgun GetWeaponTemp2;
    

    public override void Load()
    {
        player = new Player(this, (30, 30));
        camera = new Camera(this, player);
        ground = new Ground(this, (0, 5), 800);
        ground2 = new Ground(this, (40, 25), 40);
        ground3 = new Ground(this, (120, 40), 80);
        ground4 = new Ground(this, (100, 6), 10);
        box = new Box(this, (100, 41));
        GetWeaponTemp = new GetShotgun(this, (100, 20));
        GetWeaponTemp2 = new GetShotgun(this, (200, 20));

        AddGameObject(ground);
        AddGameObject(ground2);
        AddGameObject(ground3);
        AddGameObject(ground4);
        AddGameObject(new Ground(this, (101, 7), 1));
        AddGameObject(new Ground(this, (102, 8), 1));
        AddGameObject(new Ground(this, (103, 9), 1));
        AddGameObject(new Ground(this, (104, 10), 1));
        AddGameObject(new Ground(this, (105, 11), 1));
        AddGameObject(player);
        AddGameObject(camera);
        AddGameObject(box);
        AddGameObject(GetWeaponTemp);
        AddGameObject(GetWeaponTemp2);
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

        DrawGameObjects(buffer);
    }

    

    public void Error()
    {
        ToTitleRequest?.Invoke();
    }
}