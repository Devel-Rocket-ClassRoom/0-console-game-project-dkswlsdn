using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class GetHeavyMachinegun : GetWeapon
{
    public GetHeavyMachinegun(GameScene scene, Point point) : base(scene, point)
    {
        Name = "HeavyMachinegun";
        weapon = new HeavyMachinegun(Scene);
        _currentPixels = _idelPixels;
        _arms = 200;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);

        buffer.WriteText(Camera.Position + (0, 20), Position.ToString());
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