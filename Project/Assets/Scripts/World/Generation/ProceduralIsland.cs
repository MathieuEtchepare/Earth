using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralIsland : MonoBehaviour {

    public static ProceduralIsland instance = null;

    public int width = 256;
    public int height = 256;

    public int seed = 10;
    public float scale = 0.1f;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public float waterFrequency = .2f;
    public Tile[] tiles;
    public Tilemap map;
    public Tilemap collision;
    public Tilemap water;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        GenerateIsland();
        this.GetComponent<EntityManager>().GenerateAnimals();
        this.GetComponent<EntityManager>().GenerateFlowers();
    }

    void GenerateIsland()
    {
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunarity);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(noiseMap[x,y] < waterFrequency) water.SetTile(new Vector3Int(x - width/2, y - height/2, 0), tiles[2]);
                else if (noiseMap[x, y] < waterFrequency + 0.05f) map.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[1]);
                else if (noiseMap[x, y] > 0.80f) collision.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[4]);
                else if (noiseMap[x, y] > 0.60f) collision.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[3]);
                else map.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[0]);
            }
        }
    }
}
