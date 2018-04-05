using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public List<Gene> appearance = new List<Gene>();
    public List<Gene> behavior = new List<Gene>();
    public List<Gene> composition = new List<Gene>();

    protected SpriteRenderer render;

    public int scale = 15;
    private int layer = 10;

    public void Start()
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
}
