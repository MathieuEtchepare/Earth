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

        Head(appearance, pixel, Gene.GetGene(appearance, "Head W").value, Gene.GetGene(appearance, "Head H").value, 0, h - Gene.GetGene(appearance, "Ear H").value);
        Ear(appearance, pixel, Gene.GetGene(appearance, "Ear W").value, Gene.GetGene(appearance, "Ear H").value, Gene.GetGene(appearance, "Head W").value - Gene.GetGene(appearance, "Ear W").value, h);

        return pixel;
    }

    public static void Head(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        Debug.Log(x + " " + y + " " + w + " " + h);

        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                Debug.Log(i + " " + j);
                pixel[i, j] = (int)coloration.PRIMARY;
            }
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
                Debug.Log(i + " " + j);
                pixel[i, j] = (int)coloration.PRIMARY;
            }
        }

        if(Gene.GetGene(appearance, "Ear Type").value == 1) pixel[x, y - 1] = (int)coloration.NONE;
        else if (Gene.GetGene(appearance, "Ear Type").value == 2) pixel[x + w - 1, y - 1] = (int)coloration.NONE;
    }

}
