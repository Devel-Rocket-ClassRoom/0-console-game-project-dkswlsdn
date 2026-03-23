using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class EventTrigger : Entity
{
    protected bool _alreadyTriggered = false;


    public EventTrigger(GameScene scene, Point position) : base(scene, position, true)
    {
        Type = EntityType.Trigger;
        Mask = EntityType.Player;

        _canMove = false;
        _useGravity = false;
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Physics.IsOverrap(this, Scene.player) && !_alreadyTriggered)
        {
            WhenOverrap();
        }
    }

    protected abstract void WhenOverrap();
}