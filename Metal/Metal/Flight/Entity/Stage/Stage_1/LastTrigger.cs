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

        _elite = new Boss(Scene, (470, 40), EnemyState.Search, PlayerReferance);
        _elite.IsActive = false;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

    }


    protected override void ClearStageEvent()
    {
        Camera.LockLeftClamp = false;
        Camera.RightClamp = 520;
        Destroy();
    }

    protected override void WhenOverrap()
    {
        if (_alreadyTriggered) return;

        _alreadyTriggered = true;
        _elite.IsActive = true;
        Scene.AddGameObject(_elite);
        Camera.LockLeftClamp = true;
    }
}