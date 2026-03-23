using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public static class Ground
{
    public static void DrawBottomGround(StageScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i += 5)
        {
            SetGround(scene, position + (i, 0));
        }
    }

    public static void DrawNormalGround(StageScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i += 5)
        {
            SetGround(scene, position + (i, 0));
        }
    }

    public static void DrawNormalPlatform(StageScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i += 5) 
        {
            SetPlatform(scene, position + (i, 0));
        }
    }

    public static void DrawNormalWall(StageScene scene, Point position, int height)
    {
        for (int i = 0; i < height; i += 5)
        {
            SetGround(scene, position + (0, i));
        }
    }

    public static void DrawThinWall(StageScene scene, Point position, int height)
    {
        for (int i = 0; i < height; i++)
        {
            SetWall(scene, position + (0, i));
        }
    }





    public static void SetGround(StageScene scene, Point position, bool isSmall = false)
    {
        scene.AddGameObject(new GroundEntity(scene, position, isSmall));
    }
    public static void SetPlatform(StageScene scene, Point position)
    {
        scene.AddGameObject(new PlatformEntity(scene, position));
    }
    public static void SetWall(StageScene scene, Point position)
    {
        scene.AddGameObject(new CameraWall(scene, position));
    }
}