using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Ending : GameObject
{
    public Ending(GameScene scene, Point position) : base(scene, position)
    {
        _currentPixels = ending;
    }

    public override void Update(float deltaTime)
    {
        
    }

    string[] ending =
    {
        " WWWW   W   W     W WWWW   WWWW W   W WWW ",
        "W      W W  WW   WW W      W    WW  W W  W",
        "W WWW WWWWW W W W W WWWW   WWWW W W W W  W",
        "W   W W   W W  W  W W      W    W  WW W  W",
        " WWWW W   W W     W WWWW   WWWW W   W WWW ",
        "                                          ",
        "   W   W  WW  W  W   W W W WWW W   W      ",
        "    W W  W  W W  W   W W W  W  WW  W      ",
        "     W   W  W W  W   W W W  W  W W W      ",
        "     W   W  W W  W   W W W  W  W  WW      ",
        "     W    WW   WW     W W  WWW W   W      ",
    };
}