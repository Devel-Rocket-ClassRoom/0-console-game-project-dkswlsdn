using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public abstract class Weapon : GameObject
{
    public int Arms;
    public bool _isMain;

    public CharacterEntity Owner { protected get; set; }
    public BulletEntity Arm;
    protected float _recoil;
    public float Cooldown = 1f;
    protected float _leftCooldown = 0;

    protected ConsoleKey _key;

    public Weapon(Scene scene, bool isMain) : base(scene, (0, 0))
    {
        _isMain = isMain;
        _key = _isMain ? ConsoleKey.LeftArrow : ConsoleKey.RightArrow;
    }

    public override void Update(float deltaTime)
    {
        _leftCooldown -= deltaTime;
    }

    public void Drop()
    {
        Scene.RemoveGameObject(this);
    }

    public abstract float Fire(Point dir);

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(Camera.Position, Cooldown.ToString());
    }
}