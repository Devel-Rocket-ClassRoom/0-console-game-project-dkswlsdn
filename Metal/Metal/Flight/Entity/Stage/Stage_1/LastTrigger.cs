using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class LastTrigger : StageTrigger
{
    private float _reinforcementCooldown = 0;
    private const float k_ReinforcementInterval = 5f;

    public LastTrigger(GameScene scene, int position) : base(scene, (position, 0))
    {
        Width = 5;
        Height = 80;

        _elite = new Boss(Scene, (570, 40));
        _elite.IsActive = false;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

    }


    protected override void ClearStageEvent()
    {
        Destroy();
    }

    protected override void WhenOverrap()
    {
        if (_alreadyTriggered) return;

        _alreadyTriggered = true;
        _elite.IsActive = true;
        Scene.AddGameObject(_elite);
        Camera.LeftClamp = 440;
        Camera.RightClamp = 440;
        Camera.LockLeftClamp = true;
    }

    protected override bool IsEventClear()
    {
        return _elite.State == EnemyState.Dead;
    }
}