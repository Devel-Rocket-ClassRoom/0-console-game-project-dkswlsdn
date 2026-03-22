using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class GetWeapon : EventTrigger
{
    protected int _arms;
    protected Weapon weapon;
    

    public GetWeapon(Scene scene, Point point, int arms) : base(scene, point)
    {
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
        if (_player.mainWeapon.Name == Name)
        {
            _player.mainWeapon.Arms += _arms;
        }
        else
        {
            weapon.Owner = _player;
            _player.mainWeapon.Drop();
            _player.mainWeapon = weapon;
            weapon.Arms = _arms;
            Scene.AddGameObject(weapon);
        }

        Destroy();
    }
}