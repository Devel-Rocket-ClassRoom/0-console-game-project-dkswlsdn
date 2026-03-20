using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetShotgun : GetWeapon
{
    public GetShotgun(Scene scene, Point point) : base(scene, point, 20, 10)
    {
        Name = "Shotgun";
        _currentPixels = _idelPixels;
    }

    protected override void Get(Player p)
    {
        if (p.mainWeapon.Name == Name)
        {
            p.mainWeapon.Arms += 10;
        }
        else
        {
            p.mainWeapon.Drop();
            p.mainWeapon = new Shotgun(Scene, p);
            p.mainWeapon.Arms = _arms;
            Scene.AddGameObject(p.mainWeapon);
        }
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