using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class HitEffect : Entity
{
    float life = 0.1f;
    public HitEffect(Scene scene, Point point) : base(scene, point)
    {
        Scene.AddGameObject(this);
        _currentPixels = _hitPixels;
    }

    public override void Update(float deltaTime)
    {
        life -= deltaTime;

        if (life <= 0)
        {
            Scene.RemoveGameObject(this);
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawEntity(buffer);
    }

    public string[] _hitPixels =
    {
        "  y  ",
        " YyY ",
        "yyYyy",
        " YyY ",
        "  y  ",
    };
}