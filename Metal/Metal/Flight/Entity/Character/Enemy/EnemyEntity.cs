using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class EnemyEntity : CharacterEntity
{
    protected Random rand = new Random();

    protected float _attackTimer = 0f;
    protected float _attackDuration = 1.2f;
    protected float _attackBeforeDelay;

    protected float _stunTimer = 0f;
    protected float _stunDuration = 0.5f;

    protected float _deadTimer = 0f;
    protected float _deadDuration = 0.3f;

   
    protected float _reconizePlayer;

    protected Weapon _arms;
    protected GetWeapon _dropItem;


    public EnemyState State { get; protected set; }



    public Point Aim { get; protected set; }



    public EnemyEntity(GameScene scene, Point point, GetWeapon dropItem, EnemyState state = EnemyState.Idle) : base(scene, point)
    {
        State = state;

        if (dropItem != null)
        {
            dropItem.IsActive = false;
            _dropItem = dropItem;
        }
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }


    protected void DropItem()
    {
        if (_dropItem != null)
        {
            _dropItem.IsActive = true;
            _dropItem.Position = Position;
            Scene.AddGameObject(_dropItem);
        }
    }


    

    protected string[] _idlePixels =
    {
        " ggg ",
        " gCC ",
        " gCC ",
        "ggggg",
        "ggggg",
        "ggggg",
        "BgggB",
        " g g ",
        " g g ",
        " g g ",
        " B B ",
    };

    protected string[] _PanicPixels =
    {
        "B ggg B",
        "g gCC g",
        "g gCC g",
        " ggggg ",
        "  ggg  ",
        "  ggg  ",
        "  ggg  ",
        "  g g  ",
        " g   g ",
        " g   g ",
        "B     B",
    };

    protected string[] _combatPixels =
    {
        "  ggg  ",
        "  gCC  ",
        "  gCC  ",
        " ggggg ",
        "g ggg g",
        "g ggg g",
        "B ggg B",
        "  g g  ",
        "  g g  ",
        "  g g  ",
        "  B B  ",
    };

    protected string[] _deadPixels =
    {
        "           ",
        "           ",
        "           ",
        "           ",
        "           ",
        "           ",
        "   gggB    ",
        "gCCgggggggB",
        "gCCgggg    ",
        "ggggggggggB",
        "   gggB    ",
    };
}