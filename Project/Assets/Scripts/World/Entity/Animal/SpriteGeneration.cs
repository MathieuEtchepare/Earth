using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteGeneration {

    enum coloration {NONE, PRIMARY, SECONDARY, BLACK, WHITE};

	public static int[,] Corpse(int w, int h, int x, int y)
    {
        int[,] pixel = new int[w, h];

        for(int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                pixel[i, j] = (int)coloration.PRIMARY;
            }
        }
        return pixel;
    }
}
