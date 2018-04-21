using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteGeneration {

    enum coloration {NONE, PRIMARY, SECONDARY, BLACK, WHITE};

    //0/n   n/n
    //0/0   n/0

    public static int[,] Entire(List<Gene> appearance, int w, int h)
    {
        int[,] pixel = new int[w, h];

        int[] ear = { Gene.GetGene(appearance, "Ear W").value, Gene.GetGene(appearance, "Ear H").value};
        if (ear[0] == 0 || ear[1] == 0)
        {
            ear[0] = 0;
            ear[1] = 0;
        }

        int[] tail = { Gene.GetGene(appearance, "Tail W").value, Gene.GetGene(appearance, "Tail H").value };
        if (tail[0] == 0 || tail[1] == 0)
        {
            tail[0] = 0;
            tail[1] = 0;
        }

        Head(appearance, pixel, Gene.GetGene(appearance, "Head W").value, Gene.GetGene(appearance, "Head H").value, 0, h - ear[1]);
        Ear(appearance, pixel, ear[0], ear[1], Gene.GetGene(appearance, "Head W").value - ear[0], h);
        Body(appearance, pixel, Gene.GetGene(appearance, "Body W").value, Gene.GetGene(appearance, "Body H").value, Gene.GetGene(appearance, "Head W").value, h - ear[1] - Gene.GetGene(appearance, "Head H").value + 2);
        Leg(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, Gene.GetGene(appearance, "Head W").value, h - ear[1] - Gene.GetGene(appearance, "Head H").value - Gene.GetGene(appearance, "Body H").value + 3);
        Leg(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, Gene.GetGene(appearance, "Head W").value + Gene.GetGene(appearance, "Body W").value - Gene.GetGene(appearance, "Paws W").value, h - ear[1] - Gene.GetGene(appearance, "Head H").value - Gene.GetGene(appearance, "Body H").value + 3);
        Tail(appearance, pixel, tail[0], tail[1], Gene.GetGene(appearance, "Head W").value + Gene.GetGene(appearance, "Body W").value, h - ear[1] - Gene.GetGene(appearance, "Head H").value + 2);

        return pixel;
    }

    public static void Head(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
            pixel[i, y - h] = (int)coloration.SECONDARY;
        }

        pixel[x + 1, y - 2] = (int)coloration.BLACK;
    }

    public static void Ear(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        if (w == 0 || h == 0) return;
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
        }

        if(Gene.GetGene(appearance, "Ear Type").value == 1) pixel[x, y - 1] = (int)coloration.NONE;
        else if (Gene.GetGene(appearance, "Ear Type").value == 2) pixel[x + w - 1, y - 1] = (int)coloration.NONE;
    }

    public static void Body(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
            if (Gene.GetGene(appearance, "Body Type").value == 1) pixel[i, y - h] = (int)coloration.SECONDARY;
            else if (Gene.GetGene(appearance, "Body Type").value == 2 && i % 2 == 0) pixel[i, y - 1] = (int)coloration.SECONDARY;
            else if (Gene.GetGene(appearance, "Body Type").value == 3 && i % 3 == 1)
            {
                pixel[i, y - 1] = (int)coloration.SECONDARY;
                if(pixel[i - 1, y - 2] == (int)coloration.PRIMARY) pixel[i - 1, y - 2] = (int)coloration.SECONDARY;
            }
        }
    }

    public static void Leg(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
            if (Gene.GetGene(appearance, "Paws Type").value == 1 || Gene.GetGene(appearance, "Paws Type").value == 2) pixel[i, y - h] = (int)coloration.SECONDARY;
        }
        if (Gene.GetGene(appearance, "Paws Type").value == 2) pixel[x, y - h + 1] = (int)coloration.SECONDARY;

    }
    public static void Tail(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        if (w == 0 || h == 0) return;
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
        }
    }


}
