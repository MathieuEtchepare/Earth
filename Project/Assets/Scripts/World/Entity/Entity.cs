using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public List<Gene> appearance = new List<Gene>();
    public List<Gene> behavior = new List<Gene>();
    public List<Gene> composition = new List<Gene>();

    protected SpriteRenderer render;

    public int scale = 10;
    private int layer = 10;

    public int seed = 0;

    public Vector2 coord;

    public void Start()
    {
        transform.position = coord;
        generateGenome(new System.Random(seed));
        CreateRenderer();
    }
    public void CreateRenderer()
    {
        if (GetComponent<SpriteRenderer>())
        {
            render = GetComponent<SpriteRenderer>();
        }
        else
        {
            gameObject.AddComponent<SpriteRenderer>();
            render = GetComponent<SpriteRenderer>();
        }

        transform.localScale = new Vector3(scale, scale, 0);
        render.sortingOrder = layer;

        Texture2D texture = GenerateTexture();
        render.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite.name = "Texture";
    }

    public abstract Texture2D GenerateTexture();
    public abstract void generateGenome(System.Random prng);


}
