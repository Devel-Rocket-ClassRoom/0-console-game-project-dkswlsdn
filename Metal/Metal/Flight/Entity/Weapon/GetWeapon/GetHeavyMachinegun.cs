using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetHeavyMachinegun : GetWeapon
{
    public GetHeavyMachinegun(Scene scene, Point point) : base(scene, point, 200, 150)
    {
        Name = "HeavyMachinegun";
        _currentPixels = _idelPixels;
    }

    protected override void Get(Player p)
    {
        if (p.mainWeapon.Name == Name)
        {
            p.mainWeapon.Arms += _additionalArms;
        }
        else
        {
            p.mainWeapon.Drop();
            p.mainWeapon = new HeavyMachinegun(Scene, p);
            p.mainWeapon.Arms = _arms;
            Scene.AddGameObject(p.mainWeapon);
        }
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