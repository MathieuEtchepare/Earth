﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Entity
{
    public float breathSpeed = 0.02f;
    private bool up = true;

    public int life;

    public Animal()
    {
    }

    public Animal(Animal dad, Animal mom)
    {

    }

    public void Update()
    {
        Breath();
    }

    private void Breath()
    {
        if (transform.localScale.x < scale - scaleUp) up = true;
        else if(transform.localScale.x > scale + scaleUp) up = false;

        if (up) transform.localScale = new Vector3(transform.localScale.x + breathSpeed, transform.localScale.y + breathSpeed, 0);
        else transform.localScale = new Vector3(transform.localScale.x - breathSpeed, transform.localScale.y - breathSpeed, 0);

        int oxygene = ProceduralIsland.instance.GetComponent<Atmosphere>().oxygene;
        if (oxygene > 0)
        {
            ProceduralIsland.instance.GetComponent<Atmosphere>().oxygene -= Gene.GetGene(composition, "Breath").value;
            ProceduralIsland.instance.GetComponent<Atmosphere>().co2 += Gene.GetGene(composition, "Breath").value;
        }
        else life--;
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
        int tail = Gene.GetGene(appearance, "Tail W").value;
        if (Gene.GetGene(appearance, "Tail H").value == 0) tail = 0;
        return tail + Gene.GetGene(appearance, "Body W").value + Gene.GetGene(appearance, "Head W").value;
    }

    public int DetermineHeight()
    {
        int ear = Gene.GetGene(appearance, "Ear H").value;
        if (Gene.GetGene(appearance, "Ear W").value == 0) ear = 0;
        return  ear + Gene.GetGene(appearance, "Body H").value + Gene.GetGene(appearance, "Paws H").value + Gene.GetGene(appearance, "Head H").value - 3;
    }

    public override void generateGenome(System.Random prng)
    {
        composition.Add(new Gene("Life", 1, 100, true, prng));
        life = Gene.GetGene(composition, "Life").value;
        composition.Add(new Gene("Breath", 0, 10, true, prng));

        appearance.Add(new Gene("Ear W", 0, 3, true, prng));
        appearance.Add(new Gene("Ear H", 0, 3, true, prng));
        appearance.Add(new Gene("Ear Type", 0, 2, true, prng));
        appearance.Add(new Gene("Tail W", 0, 3, true, prng));
        appearance.Add(new Gene("Tail H", 0, 3, true, prng));
        appearance.Add(new Gene("Body W", 6, 12, true, prng));
        appearance.Add(new Gene("Body H", 4, 7, true, prng));
        appearance.Add(new Gene("Body Type", 0, 3, true, prng));
        appearance.Add(new Gene("Paws W", 2, (int)(Gene.GetGene(appearance, "Body W").value / 2 - 1), true, prng));
        appearance.Add(new Gene("Paws H", 2, 7, true, prng));
        appearance.Add(new Gene("Paws Type", 0, 2, true, prng));
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
