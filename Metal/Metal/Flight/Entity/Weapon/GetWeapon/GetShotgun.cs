using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetShotgun : GetWeapon
{
    public GetShotgun(GameScene scene, Point point) : base(scene, point)
    {
        Name = "Shotgun";
        _currentPixels = _idelPixels;
       weapon = new Shotgun(scene);
        _arms = 10;
    }

    

    private string[] _idelPixels =
    {
        "GGGGGGGGG",
        "GgggggggG",
        "GgYYYYYgG",
        "GgYgggggG",
        "GgYYYYYgG",
        "GgggggYgG",
        "GgYYYYYgG",
        "GgggggggG",
        "GGGGGGGGG",
    };
}