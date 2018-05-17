﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Flower : Entity
{
    public int age, year;
    public float life, currLife;

    public Flower()
    {

    }

    public Flower(Flower dad, Flower mom)
    {
    }

    public void Update()
    {
        
    }

    public override Texture2D GenerateTexture()
    {
        int width = DetermineWidth();
        int height = DetermineHeight();

        Texture2D texture = new Texture2D(width, height);

        int[,] sprite = FlowerSpriteGeneration.Entire(appearance, width, height);


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
        int lWidth = Gene.GetGene(appearance, "Leaf W").value;
        return 5;
        return lWidth + (lWidth - 1);
    }

    public int DetermineHeight()
    {
        return Gene.GetGene(appearance, "Stalk H").value + 3;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(this.transform.position, new Vector3(vision, vision, 1));
    }

    public override void generateGenome(System.Random prng)
    {
        year = ProceduralIsland.instance.GetComponent<TimeManagement>().actual_year;

        //Appearance
        appearance.Add(new Gene("Stalk H", 3, 5, true, prng));
        appearance.Add(new Gene("Stalk W", 1, 1, true, prng));

        appearance.Add(new Gene("Leaf H", 1, Gene.GetGene(appearance, "Stalk H").value - 1, true, prng));
        appearance.Add(new Gene("Leaf W", 1, 3, true, prng));

        appearance.Add(new Gene("Bloom", 0, 3, true, prng));

        appearance.Add(new Gene("red_1", 0, 255, true, prng));
        appearance.Add(new Gene("green_1", 0, 255, true, prng));
        appearance.Add(new Gene("blue_1", 0, 255, true, prng));

        appearance.Add(new Gene("red_2", 0, 255, true, prng));
        appearance.Add(new Gene("green_2", 0, 255, true, prng));
        appearance.Add(new Gene("blue_2", 0, 255, true, prng));

        //Composition
        composition.Add(new Gene("Syllable Number", 2, 4, true, prng));
        composition.Add(new Gene("Syllable 0", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 1", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 2", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 3", 0, 19, true, prng));
    }
}
