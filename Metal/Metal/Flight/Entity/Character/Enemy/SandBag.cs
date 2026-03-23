using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

public class SandBag : EnemyEntity
{
    public SandBag(GameScene scene, Point point, int dropRate, EnemyState state = EnemyState.Idle) : base(scene, point, dropRate, state)
    {
        Type = EntityType.Enemy;
        Mask = EntityType.Bullet | EntityType.Ground | EntityType.Platform;

        Width = 5;
        Height = 11;
        _canMove = true;
        _useGravity = true;

        _dropRate = dropRate;

        _arms = new EnemyGranade(scene, this);

        _currentPixels = _combatPixels;
        PlayerReferance = Scene.player;

        Health = 1;
        _reconizePlayer = 70;
        _attackBeforeDelay = 0.4f;
        _attackDuration = 3f;
        Direction = (-1, 0);
    }

    public override bool IsOutOfCamera()
    {
        return false;
    }
}