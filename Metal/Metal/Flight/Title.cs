using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Title : GameObject
{
    public Title(Scene scene, Point position) : base(scene, position)
    {
        _currentPixels = title;
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        base.Draw(buffer);
    }

    string[] title =
    {
        "      WWWWW       WWWWW              WWWWWWWWWWWWWWWWWWWWWWWW   ",
        "      WWWWW       WWWWW             WWWWWWWWWWWWWWWWWWWWWWWWW   ",
        "      WWWWW       WWWWW            WWWWWWWWWWWWWWWWWWWWWWWWWW   ",
        "      WWWWW       WWWWW            WWWWWWWWWWWWWWWWWWWWWWWWWW   ",
        "     WWWWWBW     WWWWWBW           WWWWWWWWWWWWWWWWWWWWWWWWWW   ",
        "     WWWWWBW     WWWWWBW            BBBBB                       ",
        "     WWWWWBW     WWWWWBW             WWWWW                      ",
        "     WWWWWBW     WWWWWBW              WWWWWW                    ",
        "    WWWWWBWWW   WWWWWBWWW              WWWWWW                   ",
        "    WWWWWBWWW   WWWWWBWWW               WWWWWW                  ",
        "    WWWWWBWWW   WWWWWBWWW                WWWWWW                 ",
        "    WWWWWBWWW   WWWWWBWWW                 WWWWWW                ",
        "   WWWWWBWWWWWBWWWWWBWWWWW                 WWWWWW               ",
        "   WWWWWBWWWWWBWWWWWBWWWWW                  WWWWWW              ",
        "   WWWWWBWWWWWBWWWWWBWWWWW                   WWWWWW             ",
        "   WWWWWBWWWWWBWWWWWBWWWWW                    WWWWWW            ",
        "  WWWWW   WWWWWBWWW   WWWWW                    WWWWWW           ",
        "  WWWWW   WWWWWBWWW   WWWWW                     WWWWWW          ",
        "  WWWWW   WWWWWBWWW   WWWWW                      WWWWWW         ",
        "  WWWWW   WWWWWBWWW   WWWWW                       WWWWWW        ",
        " WWWWW     WWWWWBW     WWWWW                       WWWWWW       ",
        " WWWWW     WWWWWBW     WWWWW                        WWWWWW      ",
        " WWWWW     WWWWWBW     WWWWW                         WWWWWW     ",
        " WWWWW     WWWWWBW     WWWWW      WWWWWWWWWWWWWWWWWWWBWWWWWW    ",
        "WWWWW       WWWWW       WWWWW     WWWWWWWWWWWWWWWWWWWWBWWWWWW   ",
        "WWWWW       WWWWW       WWWWW     WWWWWWWWWWWWWWWWWWWWWBWWWWW   ",
        "WWWWW       WWWWW       WWWWW     WWWWWWWWWWWWWWWWWWWWWWBWWW    ",
        "WWWWW       WWWWW       WWWWW     WWWWWWWWWWWWWWWWWWWWWWWBW     ",
        "                                                                ",
        "                                                                ",
        "                                                                ",
        "                                                                ",
        "                                                                ",
        "                                                                ",
        "                                                                ",
        "  WWW  WWW  WWWW WWWW WWWW         WWWW W   W WWWWW WWWW WWW    ",
        "  W  W W  W W    W    W            W    WW  W   W   W    W  W   ",
        "  WWW  WWW  WWWW WWWW WWWW         WWWW W W W   W   WWWW WWW    ",
        "  W    W  W W       W    W         W    W  WW   W   W    W  W   ",
        "  W    W  W WWWW WWWW WWWW         WWWW W   W   W   WWWW W  W   ",
    };
}