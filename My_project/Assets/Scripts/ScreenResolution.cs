using System;
using UnityEngine;

public static class SRes
{
    public static int width = 1080;
    public static int height = 1920;

    public static float Width(float size)
    {
        return width / 1080 * size;
    }
    public static float Height(float size)
    {
        return height / 1080 * size;
    }
    public static Vector2 Size(float width, float height)
    {
        return new Vector2 (Width(width), Height(height));
    }
}