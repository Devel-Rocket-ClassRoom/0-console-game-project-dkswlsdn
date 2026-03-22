using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;


public static class Ground
{
    public static void DrawBottomGround(GameScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i += 5)
        {
            SetGround(scene, position + (i, 0));
        }
    }

    public static void DrawNormalGround(GameScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                SetGround(scene, position + (i, j));
            }
        }
    }

    public static void DrawNormalPlatform(GameScene scene, Point position, int width)
    {
        for (int i = 0; i < width; i += 5) 
        {
            SetGround(scene, position + (i, 0));
        }
    }

    public static void DrawNormalWall(GameScene scene, Point position, int height)
    {
        for (int i = 0; i < height; i += 5)
        {
            SetGround(scene, position + (0, i));
        }
    }

    public static void DrawThinWall(GameScene scene, Point position, int height)
    {
        for (int i = 0; i < height; i++)
        {
            SetWall(scene, position + (0, i));
        }
    }





    public static void SetGround(GameScene scene, Point position, bool isSmall = false)
    {
        scene.AddGameObject(new GroundEntity(scene, position, isSmall));
    }
    public static void SetPlatform(GameScene scene, Point position)
    {
        scene.AddGameObject(new PlatformEntity(scene, position));
    }
    public static void SetWall(GameScene scene, Point position)
    {
        scene.AddGameObject(new CameraWall(scene, position));
    }
}