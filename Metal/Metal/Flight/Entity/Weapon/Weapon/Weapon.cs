using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class Weapon : GameObject
{
    public string Name { get; set; }
    public int Arms;
    public bool _isMain;

    protected CharacterEntity _ownerID;
    public AttackEntity Arm;
    public float Cooldown = 1;
    private float _leftCooldown = 0;

    private ConsoleKey _key;

    public Weapon(Scene scene, CharacterEntity id, bool isMain) : base(scene)
    {
        _isMain = isMain;
        _ownerID = id;
        _key = _isMain ? ConsoleKey.LeftArrow : ConsoleKey.RightArrow;
    }

    public override void Update(float deltaTime)
    {
        if (_leftCooldown <= 0 && Input.IsKeyDown(_key) && Arms > 0)
        {
            Fire();
            _leftCooldown = Cooldown;
        }

        _leftCooldown -= deltaTime;
    }

    private void Fire()
    {
        if (Scene is GameScene g)
        {
            Scene.AddGameObject(GetArms());
        }
    }

    public void Drop()
    {
        Scene.RemoveGameObject(this);
    }

    protected abstract AttackEntity GetArms();

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(Camera.Position, Cooldown.ToString());
    }
}