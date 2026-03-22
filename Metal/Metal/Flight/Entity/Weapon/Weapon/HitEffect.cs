using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class HitEffect : GameObject
{
    float life = 0.1f;
    public HitEffect(Scene scene, Point point) : base(scene, point)
    {
        _currentPixels = _hitPixels;
    }

    public override void Update(float deltaTime)
    {
        life -= deltaTime;

        if (life <= 0)
        {
            Destroy();
        }
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