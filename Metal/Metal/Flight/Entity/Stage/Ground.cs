using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Ground : GroundEntity
{
    protected int _width;
    public bool IsPlatForm { get; private set; }


    public Ground(Scene scene, Point point, int width, string name = "g", bool canHit = true, bool isPlatform = false) : base(scene, point + (width / 2, -3), canHit)
    {
        if (Scene is GameScene g)
        {
            g.GroundEntitiyList.Add(this);
            g.GroundEntitiyList.Sort((a, b) => b.Position.Y.CompareTo(a.Position.Y));
        }

        RectAngle = new RectAngle(this, (width, 5));
        Name = name;
        _width = width;

        IsPlatForm = isPlatform;

        new Wall(scene, point + (0, -4), 5, canHit: canHit);
        new Wall(scene, point + (width, -4), 5, canHit: canHit);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        RectAngle.DrawRectAngle(buffer);

        for (int i = 0; i < _width; i += 100)
        {
            buffer.WriteText(Position + (i, 0), i.ToString(), ConsoleColor.White, ConsoleColor.DarkBlue);
        }
    }
    
    public override void Update(float deltaTime)
    {
        RectAngle.Follow(Position);
    }
}