using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Entity
{

    public override Texture2D GenerateTexture()
    {
        int width = 5;
        int height = 5;

        Texture2D texture = new Texture2D(width, height);

        int[,] sprite = SpriteGeneration.Corpse(width, height, 0, 0);

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                texture.SetPixel(i, j, new Color(255, 0, 0));
            }
        }
        texture.Apply();
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        return texture;
    }

    public void generateGenome()
    {
        appearance.Add(new Gene("Ear W", 0, 2, true));
    }
}
