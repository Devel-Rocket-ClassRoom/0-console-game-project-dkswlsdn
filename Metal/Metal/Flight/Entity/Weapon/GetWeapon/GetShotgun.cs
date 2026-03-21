using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetShotgun : GetWeapon
{
    public GetShotgun(Scene scene, Point point) : base(scene, point, 20)
    {
        Name = "Shotgun";
        _currentPixels = _idelPixels;
       weapon = new Shotgun(scene);
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