using UnityEngine;

public static class PerlinNoise{
    public static float[,] GenerateNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[width, height];
        System.Random prng = new System.Random(seed);

        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 10000);
            float offsetY = prng.Next(-100000, 10000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) scale = 0.001f;

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++)
                {
                    float perlinValue = CalculatePerlinNoise(x, y, scale, octaveOffsets[i].x, octaveOffsets[i].y, frequency);
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]) * CalculateIslandPelinNoise(x, y, width, height);
            }
        }

        return noiseMap;
    }

    public static float CalculatePerlinNoise(int x, int y, float scale, float offsetX, float offsetY, float frequency)
    {
        float xCoord = (float)x / scale * frequency + offsetX;
        float yCoord = (float)y / scale * frequency + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
    }

    // Add a circular mask to the perlin noise to have "round border"
    public static float CalculateIslandPelinNoise(int x, int y, int width, int height)
    {
        float oceanDistance = 50f; // The ocean area around island
        float miWidth = width / 2;
        float miHeight = height / 2;
        float xCenter = x - miWidth; // Center of the circle (0;0) instead of (width/2; height/2)
        float yCenter = y - miHeight;

        float distance = Mathf.Sqrt(xCenter * xCenter + yCenter * yCenter);
        float maxDistance = Mathf.Sqrt(miWidth * miWidth + miHeight * miHeight) - oceanDistance;
        float delta = distance / maxDistance;

        float value = (1 - delta) * 1.5f;

        return Mathf.Clamp(value, 0f, 1f);
    }

}
