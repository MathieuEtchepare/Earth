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

        int xBody, yBody;

        switch (Gene.GetGene(appearance, "Head Pos").value)
        {
            case 0:
                xBody = Gene.GetGene(appearance, "Head W").value;
                yBody = h - ear[1];
                break;
            case 1:
                xBody = 0;
                yBody = h - ear[1] - Gene.GetGene(appearance, "Head H").value;
                break;
            default:
                xBody = Gene.GetGene(appearance, "Head W").value - 2;
                yBody = h - ear[1] - Gene.GetGene(appearance, "Head H").value + 2;
                break;
        }
        Body(appearance, pixel, Gene.GetGene(appearance, "Body W").value, Gene.GetGene(appearance, "Body H").value, xBody, yBody);
        Head(appearance, pixel, Gene.GetGene(appearance, "Head W").value, Gene.GetGene(appearance, "Head H").value, 0, h - ear[1]);

        Ear(appearance, pixel, ear[0], ear[1], 0, h);
        Ear(appearance, pixel, ear[0], ear[1], Gene.GetGene(appearance, "Head W").value - ear[0], h);

        LegBack(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, xBody + 1, yBody - Gene.GetGene(appearance, "Body H").value);
        Leg(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, xBody + 2 + Gene.GetGene(appearance, "Paws W").value, yBody - Gene.GetGene(appearance, "Body H").value);
        LegBack(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, xBody + Gene.GetGene(appearance, "Body W").value - (Gene.GetGene(appearance, "Paws W").value * 2 + 1), yBody - Gene.GetGene(appearance, "Body H").value);
        Leg(appearance, pixel, Gene.GetGene(appearance, "Paws W").value, Gene.GetGene(appearance, "Paws H").value, xBody + Gene.GetGene(appearance, "Body W").value - Gene.GetGene(appearance, "Paws W").value, yBody - Gene.GetGene(appearance, "Body H").value);
        Tail(appearance, pixel, tail[0], tail[1], xBody + Gene.GetGene(appearance, "Body W").value, yBody);

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
            if(Gene.GetGene(appearance, "Head Type").value == 1 || Gene.GetGene(appearance, "Head Type").value == 2) pixel[i, y - h] = (int)coloration.SECONDARY;
            else if (Gene.GetGene(appearance, "Head Type").value == 3 && i%2 == 0) pixel[i, y - h] = (int)coloration.SECONDARY;
            else if (Gene.GetGene(appearance, "Head Type").value == 5 && i != x + w - 1) pixel[i, y - h] = (int)coloration.BLACK;
        }

        if (Gene.GetGene(appearance, "Head Type").value == 2) pixel[x, y - 1] = (int)coloration.SECONDARY;
        pixel[x, y - 2] = (int)coloration.WHITE;
        pixel[x, y - 3] = (int)coloration.BLACK;
        if (Gene.GetGene(appearance, "Head Type").value == 4) pixel[x, y - 4] = (int)coloration.SECONDARY;
        if (Gene.GetGene(appearance, "Head Type").value == 6) pixel[x, y - h] = (int)coloration.WHITE;

        if (Gene.GetGene(appearance, "Head Type").value == 2) pixel[x + w - 2, y - 1] = (int)coloration.SECONDARY;
        pixel[x + w - 2, y - 2] = (int)coloration.WHITE;
        pixel[x + w - 2, y - 3] = (int)coloration.BLACK;
        if (Gene.GetGene(appearance, "Head Type").value == 4) pixel[x + w - 2, y - 4] = (int)coloration.SECONDARY;
        if (Gene.GetGene(appearance, "Head Type").value == 6) pixel[x + w - 2, y - h] = (int)coloration.WHITE;
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

        switch(Gene.GetGene(appearance, "Ear Type").value)
        {
            case 1:
                if(w == 1 && h == 1) pixel[x, y - 1] = (int)coloration.BLACK;
                else pixel[x, y - 1] = (int)coloration.NONE;
                break;
            case 2:
                if (w == 1 && h == 1) pixel[x, y - 1] = (int)coloration.BLACK;
                else pixel[x + 1, y - 1] = (int)coloration.NONE;
                break;
            case 3:
                for (int j = y - h; j < y; j++)
                {
                    pixel[x, j] = (int)coloration.NONE;
                }
                break;
            case 4:
                for (int j = y - h; j < y; j++)
                {
                    pixel[x + 1, j] = (int)coloration.NONE;
                }
                break;
            case 5:
                pixel[x, y - h] = (int)coloration.NONE;
                break;
            case 6:
                pixel[x + 1, y - h] = (int)coloration.NONE;
                break;

        }
    }

    public static void Body(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        
        Debug.Log(Random.Range(0, 4265465) + 
            " : " + Gene.GetGene(appearance, "Head Pos").value +
            " : " + Gene.GetGene(appearance, "Head W").value +
            " : " + Gene.GetGene(appearance, "Head H").value +
            " : " + Gene.GetGene(appearance, "Body W").value +
            " : " + Gene.GetGene(appearance, "Body H").value);
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
        }
        if (Gene.GetGene(appearance, "Paws Type").value == 1) pixel[x, y - h] = (int)coloration.SECONDARY;
    }

    public static void LegBack(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        for (int i = x; i < x + w; i++)
        {
            for (int j = y - h; j < y; j++)
            {
                pixel[i, j] = (int)coloration.BLACK;
            }
        }

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
