using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class StageTrigger : EventTrigger
{
    protected EnemyEntity _elite;

    public StageTrigger(GameScene scene, Point position) : base(scene, position)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (IsEventClear())
        {
            ClearStageEvent();
        }
    }

    protected abstract void ClearStageEvent();

    protected abstract bool IsEventClear();
}