﻿using System.Collections;
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

    public BoxCollider bc;
   

    public void Start()
    {
        transform.position = coord;
        generateGenome(new System.Random(seed));
        CreateRenderer();
        BoxCollider bc = gameObject.AddComponent<BoxCollider>();
        gameObject.tag = "Entity";
        gameObject.name = NameGenerator.GenerateName(composition);
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
    public abstract void generateGenome(System.Random prng);


}
