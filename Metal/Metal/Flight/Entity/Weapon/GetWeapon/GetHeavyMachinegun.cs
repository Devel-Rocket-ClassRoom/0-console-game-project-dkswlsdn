using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetHeavyMachinegun : GetWeapon
{
    public GetHeavyMachinegun(Scene scene, Point point) : base(scene, point, 200)
    {
        Name = "HeavyMachinegun";
        weapon = new HeavyMachinegun(Scene);
        _currentPixels = _idelPixels;
    }


    private string[] _idelPixels =
    {
        "GGGGGGGGG",
        "GBBBBBBBG",
        "GBYBBBYBG",
        "GBYBBBYBG",
        "GBYYYYYBG",
        "GBYBBBYBG",
        "GBYBBBYBG",
        "GBBBBBBBG",
        "GGGGGGGGG",
    };
}