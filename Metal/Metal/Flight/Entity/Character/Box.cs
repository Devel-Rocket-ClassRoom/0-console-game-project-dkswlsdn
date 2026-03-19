using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public class Box : CharacterEntity
{
    public Box(Scene scene, Point point) : base(scene, point)
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(int attackId, int damage, int immuneDuration)
    {
        throw new NotImplementedException();
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
}