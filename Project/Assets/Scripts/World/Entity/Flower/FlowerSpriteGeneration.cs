using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowerSpriteGeneration {

    enum coloration {NONE, PRIMARY, SECONDARY, BLACK, WHITE};

    //0/n   n/n
    //0/0   n/0

    public static int[,] Entire(List<Gene> appearance, int w, int h)
    {
        int[,] pixel = new int[w, h];

        Stalk(appearance, pixel, Gene.GetGene(appearance, "Stalk W").value, Gene.GetGene(appearance, "Stalk H").value, (int)Mathf.Floor(w/2), h);
        //Body(appearance, pixel, Gene.GetGene(appearance, "Body W").value, Gene.GetGene(appearance, "Body H").value, xBody, yBody);
        Leaf(appearance, pixel, Gene.GetGene(appearance, "Leaf W").value, Gene.GetGene(appearance, "Leaf H").value, (int)Mathf.Floor(w / 2), Gene.GetGene(appearance, "Leaf H").value);
        Bloom(appearance, pixel, 3, 3, (int)Mathf.Floor(w / 2), Gene.GetGene(appearance, "Stalk H").value);
        return pixel;
    }

    

    public static void Stalk(List<Gene> appearance, int [,] pixel, int w, int h, int x, int y)
    {
        if (w == 0 || h == 0) return;

        for(int i = 0; i < h; i++)
        {
            pixel[x, i] = (int)coloration.PRIMARY;
        }
        
    }

    public static void Leaf(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        if (w == 1 ) return;
        if(w >= 2)
        {
            pixel[x - 1, y] = (int)coloration.PRIMARY;
            pixel[x + 1, y] = (int)coloration.PRIMARY;
        }
        if (w == 3)
        {
            pixel[x - 2, y - 1] = (int)coloration.PRIMARY;
            pixel[x + 2, y - 1] = (int)coloration.PRIMARY;

        }
    }

    public static void Bloom(List<Gene> appearance, int[,] pixel, int w, int h, int x, int y)
    {
        int type = Gene.GetGene(appearance, "Bloom").value;

        for (int i = -w/2; i <= w/2; i++)
        {
            pixel[x + i, y] = (int)coloration.SECONDARY;
        }
        if (type == 1 || type == 2)
        {
            pixel[x, y - 1] = (int)coloration.SECONDARY;
            pixel[x, y + 1] = (int)coloration.SECONDARY;
        }
        if(type == 2) pixel[x, y] = (int)coloration.WHITE;
        if (type == 3) pixel[x, y + 1] = (int)coloration.SECONDARY;


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


    }

   

}
