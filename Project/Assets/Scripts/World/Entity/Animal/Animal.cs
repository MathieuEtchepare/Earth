using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Entity
{
    public Animal()
    {
        
    }

    public Animal(Animal dad, Animal mom)
    {

    }

    public override Texture2D GenerateTexture()
    {
        int width = DetermineWidth();
        int height = DetermineHeight();

        Texture2D texture = new Texture2D(width, height);

        int[,] sprite = SpriteGeneration.Entire(appearance, width, height);


        float[] colors = { Gene.GetGene(appearance, "red_1").value, Gene.GetGene(appearance, "green_1").value, Gene.GetGene(appearance, "blue_1").value,
            Gene.GetGene(appearance, "red_2").value, Gene.GetGene(appearance, "green_2").value, Gene.GetGene(appearance, "blue_2").value};

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (sprite[i, j] == 1) texture.SetPixel(i, j, new Color(colors[0]/255, colors[1]/255, colors[2]/255));
                else if (sprite[i, j] == 2) texture.SetPixel(i, j, new Color(colors[3]/255, colors[4]/255, colors[5]/255));
                else if (sprite[i, j] == 3) texture.SetPixel(i, j, new Color(0, 0, 0));
                else if (sprite[i, j] == 4) texture.SetPixel(i, j, new Color(1, 1, 1));
                else texture.SetPixel(i, j, new Color(1, 1, 1, 0));
            }
        }


        texture.Apply();
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        return texture;
    }

    public int DetermineWidth()
    {
        return Gene.GetGene(appearance, "Ear W").value + Gene.GetGene(appearance, "Tail W").value + Gene.GetGene(appearance, "Body W").value + Gene.GetGene(appearance, "Paws W").value + Gene.GetGene(appearance, "Head W").value;
    }

    public int DetermineHeight()
    {
        return Gene.GetGene(appearance, "Ear H").value + Gene.GetGene(appearance, "Tail H").value + Gene.GetGene(appearance, "Body H").value + Gene.GetGene(appearance, "Paws H").value + Gene.GetGene(appearance, "Head H").value;
    }

    public override void generateGenome(System.Random prng)
    {
        appearance.Add(new Gene("Ear W", 0, 3, true, prng));
        appearance.Add(new Gene("Ear H", 0, 3, true, prng));
        appearance.Add(new Gene("Ear Type", 0, 2, true, prng));
        appearance.Add(new Gene("Tail W", 0, 3, true, prng));
        appearance.Add(new Gene("Tail H", 0, 3, true, prng));
        appearance.Add(new Gene("Body W", 6, 12, true, prng));
        appearance.Add(new Gene("Body H", 4, 7, true, prng));
        appearance.Add(new Gene("Paws W", 1, 4, true, prng));
        appearance.Add(new Gene("Paws H", 1, 6, true, prng));
        appearance.Add(new Gene("Head W", 4, 5, true, prng));
        appearance.Add(new Gene("Head H", 4, 6, true, prng));

        appearance.Add(new Gene("red_1", 0, 255, true, prng));
        appearance.Add(new Gene("green_1", 0, 255, true, prng));
        appearance.Add(new Gene("blue_1", 0, 255, true, prng));

        appearance.Add(new Gene("red_2", 0, 255, true, prng));
        appearance.Add(new Gene("green_2", 0, 255, true, prng));
        appearance.Add(new Gene("blue_2", 0, 255, true, prng));

    }
}
