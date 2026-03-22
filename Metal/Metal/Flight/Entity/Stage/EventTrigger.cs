using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class EventTrigger : Entity
{
    protected Player _player;
    protected bool _alreadyTriggered = false;


    public EventTrigger(Scene scene, Point position) : base(scene, position, true)
    {
        Type = EntityType.Trigger;
        Mask = EntityType.Player;

        _canMove = false;
        _useGravity = false;

        if (scene is GameScene g)
        {
            _player = g.player;
        }
        else
        {
            Destroy();
        }
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Physics.IsOverrap(this, _player) && !_alreadyTriggered)
        {
            WhenOverrap();
        }
    }

    protected abstract void WhenOverrap();
}