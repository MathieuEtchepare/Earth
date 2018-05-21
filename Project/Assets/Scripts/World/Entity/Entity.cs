using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public List<Gene> appearance = new List<Gene>();
    public List<Gene> behavior = new List<Gene>();
    public List<Gene> composition = new List<Gene>();

    protected SpriteRenderer render;

    protected int scale = 10;
    protected float scaleUp = 0.5f;

    private int layer = 10;

    public int seed = 0;

    public Vector2 coord;

    public void Start()
    {
        transform.position = coord;
        gameObject.tag = "Entity";
    }

    public void Generate()
    {
        CreateRenderer();
        gameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rg2D = gameObject.AddComponent<Rigidbody2D>();
        rg2D.freezeRotation = true;
        gameObject.name = AnimalNameGenerator.GenerateName(composition);
        gameObject.layer = 8;
    }

    public void GenerateFlower()
    {
        CreateRenderer();
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        Rigidbody2D rg2D = gameObject.AddComponent<Rigidbody2D>();
        rg2D.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.name = AnimalNameGenerator.GenerateName(composition);
        gameObject.layer = 9;
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

        transform.localScale = new Vector3(Random.Range(scale - scaleUp, scale + scaleUp), Random.Range(scale - 1, scale + 1), 0);
        render.sortingOrder = layer;

        Texture2D texture = GenerateTexture();
        render.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        render.sprite.name = "Texture";
    }

    public abstract Texture2D GenerateTexture();
    public abstract void GenerateGenome(System.Random prng);


}
