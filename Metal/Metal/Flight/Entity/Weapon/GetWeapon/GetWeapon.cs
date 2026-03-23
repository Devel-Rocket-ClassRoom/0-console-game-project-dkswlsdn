using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GetWeapon : EventTrigger
{
    protected int _arms;
    protected Weapon weapon;
    

    public GetWeapon(GameScene scene, Point point) : base(scene, point)
    {
        Type = EntityType.Trigger;
        Mask = EntityType.Player;

        _canMove = true;
        _useGravity = true;

        Width = 9;
        Height = 9;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        CheckCameraBounds(false);
    }

    protected override void WhenOverrap()
    {
        if (Scene.player.mainWeapon.Name == Name)
        {
            Scene.player.mainWeapon.Arms += _arms;
        }
        else
        {
            weapon.Owner = Scene.player;
            Scene.player.mainWeapon.Drop();
            Scene.player.mainWeapon = weapon;
            weapon.Arms = _arms;
            Scene.AddGameObject(weapon);
        }

        Destroy();
    }
}