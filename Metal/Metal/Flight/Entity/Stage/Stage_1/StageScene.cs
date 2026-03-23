using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class StageScene : GameScene
{
    public event GameAction ToMenuRequest;

    public static bool IsPlayerWin = false;




    public override void Load()
    {
        base.Load();

        Ground.DrawBottomGround(this, (0, 0), 3000);
        Ground.DrawNormalWall(this, (140, 0), 25);
        Ground.DrawNormalPlatform(this, (200, 25), 50);

        Ground.DrawNormalWall(this, (260, 0), 50);
        Ground.DrawNormalGround(this, (260, 50), 100);

        
        //AddGameObject(new GetHeavyMachinegun(this, (145, 20)));
        AddGameObject(new ZeroTrigger(this, 30));
        AddGameObject(new FirstTrigger(this, 350));
        AddGameObject(new LastTrigger(this, 470));
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        UpdateGameObjects(deltaTime);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.FillRect(0, 0, ShottingGame.k_Width, ShottingGame.k_Height, bgColor: ConsoleColor.DarkCyan);
        buffer.WriteText(Camera.Position + (0, 0), _gameObjects.Count.ToString());

        DrawGameObjects(buffer);
    }



    public void Ending()
    {
        ToMenuRequest?.Invoke();
    }
}