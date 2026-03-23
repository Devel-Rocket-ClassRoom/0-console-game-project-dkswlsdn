using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class FirstTrigger : StageTrigger
{
    private float _reinforcementCooldown = 0;
    private const float k_ReinforcementInterval = 5f;

    public FirstTrigger(GameScene scene, int position) : base(scene, (position, 0))
    {
        Width = 5;
        Height = 80;

        _elite = new Tank(Scene, (250, 6), EnemyState.Search, PlayerReferance, 100);
        _elite.IsActive = false;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (_reinforcementCooldown <= 0 && _alreadyTriggered)
        {
            Scene.AddGameObject(new ModenInfantryCannon(Scene, (460, 6), EnemyState.Chase, PlayerReferance, -1));
            _reinforcementCooldown = k_ReinforcementInterval;
        }

        _reinforcementCooldown -= deltaTime;
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
        Scene.AddGameObject(new Tank(Scene, (460, 6), EnemyState.Search, PlayerReferance, 0));
        Scene.AddGameObject(_elite);
        Camera.LeftClamp = 300;
        Camera.LockLeftClamp = true;
    }
}