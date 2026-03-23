using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

internal class ZeroTrigger : StageTrigger
{

    public ZeroTrigger(GameScene scene, int position) : base(scene, (position, 0))
    {
        Width = 5;
        Height = 80;

        _elite = new Tank(Scene, (250, 6), EnemyState.Move, null);
        _elite.IsActive = false;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

    }


    protected override void ClearStageEvent()
    {
    }

    protected override void WhenOverrap()
    {
        if (_alreadyTriggered) return;

        Scene.AddGameObject(new ModenInfantryCannon(Scene, (160, 6), EnemyState.Idle, new GetHeavyMachinegun(Scene, (0, 0)), 1));
        Scene.AddGameObject(new ModenInfantryGranade(Scene, (220, 31), EnemyState.Idle, null));
        Scene.AddGameObject(new ModenInfantryCannon(Scene, (240, 31), EnemyState.Idle, null));
        Scene.AddGameObject(new ModenInfantryCannon(Scene, (240, 6), EnemyState.Idle, null));

        Destroy();
    }

    protected override bool IsEventClear()
    {
        return true;
    }
}
