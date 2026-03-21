using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class EnemyEntity : CharacterEntity
{
    protected AttackEntity _arms;
    protected int _destinationX;
    public EnemyEntity(Scene scene, Point point) : base(scene, point)
    {

    }
}