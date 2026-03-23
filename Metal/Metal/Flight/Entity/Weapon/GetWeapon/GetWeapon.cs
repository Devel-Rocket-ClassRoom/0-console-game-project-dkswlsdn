using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GetWeapon : EventTrigger
{
    protected int _arms;
    protected Weapon weapon;
    

    public GetWeapon(GameScene scene, Point point, int arms) : base(scene, point)
    {
        PlayerReferance = scene.player;

        Type = EntityType.Trigger;
        Mask = EntityType.Player;

        _canMove = true;
        _useGravity = true;

        Width = 9;
        Height = 9;

        _arms = arms;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        CheckCameraBounds(false);
    }

    protected override void WhenOverrap()
    {
        if (PlayerReferance.mainWeapon.Name == Name)
        {
            PlayerReferance.mainWeapon.Arms += _arms;
        }
        else
        {
            weapon.Owner = PlayerReferance;
            PlayerReferance.mainWeapon.Drop();
            PlayerReferance.mainWeapon = weapon;
            weapon.Arms = _arms;
            Scene.AddGameObject(weapon);
        }

        Destroy();
    }
}