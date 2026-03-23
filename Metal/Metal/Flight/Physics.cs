using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static Entity;


public static class Physics
{
    public static bool IsOverrap(Entity a, List<Entity> allEntities, out Entity collider)
    {
        for (int i = 0; i < allEntities.Count; i++)
        {
            Entity b = allEntities[i];

            if (a == b) continue;
            if (!CanCollide(a, b)) continue;


            if (a.Velocity.Y > 0 && b.Type == EntityType.Platform)
                continue;


            float diffX = a.Position.X - b.Position.X;
            float diffY = a.Position.Y - b.Position.Y;

            float distSq = (diffX * diffX) + (diffY * diffY);

            if (distSq > (a.Threshold + b.Threshold) * (a.Threshold + b.Threshold)) continue;


            if (CheckAABB(a, b))
            {
                collider = allEntities[i];
                return true;
            }
        }

        collider = null;
        return false;
    }
    public static bool IsOverrap(Entity a, Entity b)
    {
        if (b == null || !b.IsActive) return false;
        if (a == b) return false;
        if (!CanCollide(a, b)) return false;


        float diffX = a.Position.X - b.Position.X;
        float diffY = a.Position.Y - b.Position.Y;

        float distSq = (diffX * diffX) + (diffY * diffY);

        if (distSq > (a.Threshold + b.Threshold) * (a.Threshold + b.Threshold)) return false;


        return CheckAABB(a, b);
    }

    public static Entity GetOverlapObject(Point point, List<Entity> targets)
    {
        foreach (var target in targets)
        {
            // 점(Point)이 사각형(Entity) 안에 포함되는지 검사
            if (point.X >= target.Position.X && point.X < target.Position.X + target.Width &&
                point.Y >= target.Position.Y && point.Y < target.Position.Y + target.Height)
            {
                return target;
            }
        }
        return null;
    }


    public static bool CheckAABB(Entity a, Entity b)
    {
        return !(a.Left > b.Right
            || a.Right < b.Left
            || a.Bottom > b.Top
            || a.Top < b.Bottom);
    }

    public static bool CanCollide(Entity a, Entity b)
    {
        bool aWantsB = (a.Mask & b.Type) != 0;
        bool bWantsA = (b.Mask & a.Type) != 0;

        return aWantsB && bWantsA;
    }
}