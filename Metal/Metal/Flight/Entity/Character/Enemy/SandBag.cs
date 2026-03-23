using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

public class SandBag : EnemyEntity
{
    public SandBag(GameScene scene, Point point, GetWeapon dropItem, EnemyState state = EnemyState.Idle) : base(scene, point, dropItem, state)
    {
    }
}