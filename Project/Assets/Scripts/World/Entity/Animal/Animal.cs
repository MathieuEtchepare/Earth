﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Entity
{
    public float breathSpeed = 0.02f;
    private bool up = true;

    public int age, year;
    public float life, currLife;
    public float food, currFood;
    public float water, currWater;
    public int breath;
    public float speed;
    public int weight;

    public static float CHANGE_BEHAVIOR_PERIOD = 10f; //milliseconds
    private float lastChangedBehaviorTime = 0; //milliseconds
    private Vector2 direction;

    public enum Behavior { WAIT, WALK, HUNT, FOLLOW, LOVE, LEAK, WATER, EAT };
    public Behavior currBehaviour;

    public bool isAttacked = false;

    public Animal()
    {
    }

    public Animal(Animal dad, Animal mom)
    {
    }

    public void Update()
    {
        Breath();
        Envy();
        switch (currBehaviour)
        {
            case Behavior.WAIT:
                Wait();
                break;
            case Behavior.WALK:
                Walk();
                break;
            case Behavior.WATER:
                Walk();
                break;
        }
    }

    private void GenerateNextBehaviour()
    {
        if (isAttacked)
        {
            if(Gene.GetGene(appearance, "Bravery").value == 0) currBehaviour = Behavior.LEAK;
            else currBehaviour = Behavior.HUNT;
        }
        else
        {
            if(currWater < 1/10*water) currBehaviour = Behavior.WATER;
            else
            {
                if(Random.Range(0, 5) == 0) currBehaviour = Behavior.WAIT;
                else currBehaviour = Behavior.WALK;
            }
        }
    }

    public void Wait()
    {
        lastChangedBehaviorTime -= Time.deltaTime;

        if (lastChangedBehaviorTime <= 0)
        {
            lastChangedBehaviorTime = Random.Range(CHANGE_BEHAVIOR_PERIOD, CHANGE_BEHAVIOR_PERIOD * 2);
            GenerateNextBehaviour();

        }
    }
    public void Walk()
    {
        lastChangedBehaviorTime -= Time.deltaTime;
        
        if (lastChangedBehaviorTime <= 0)
        {
            lastChangedBehaviorTime = Random.Range(CHANGE_BEHAVIOR_PERIOD, CHANGE_BEHAVIOR_PERIOD * 2);
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            if (direction.x > 0) render.flipX = true;
            else render.flipX = false;

            GenerateNextBehaviour();
        }
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        direction = new Vector2(-direction.x, -direction.y);

        if (direction.x > 0) render.flipX = true;
        else render.flipX = false;
    }

    private void Breath()
    {
        if (transform.localScale.x < scale - scaleUp) up = true;
        else if(transform.localScale.x > scale + scaleUp) up = false;

        if (up) transform.localScale = new Vector3(transform.localScale.x + breathSpeed, transform.localScale.y + breathSpeed, 0);
        else transform.localScale = new Vector3(transform.localScale.x - breathSpeed, transform.localScale.y - breathSpeed, 0);
    }

    private void Envy()
    {
        if(year != ProceduralIsland.instance.GetComponent<TimeManagement>().actual_year)
        {
            currFood--;
            currWater--;

            int oxygene = ProceduralIsland.instance.GetComponent<Atmosphere>().oxygene;

            if (oxygene > 0)
            {
                ProceduralIsland.instance.GetComponent<Atmosphere>().oxygene -= breath;

                ProceduralIsland.instance.GetComponent<Atmosphere>().co2 += breath;
            }
            else currLife--;

            year = ProceduralIsland.instance.GetComponent<TimeManagement>().actual_year;
        }
    }

    public override Texture2D GenerateTexture()
    {
        int width = DetermineWidth();
        int height = DetermineHeight();

        Texture2D texture = new Texture2D(width, height);

        int[,] sprite = SpriteGeneration.Entire(appearance, width, height);


        float[] colors = { Gene.GetGene(appearance, "red_1").value, Gene.GetGene(appearance, "green_1").value, Gene.GetGene(appearance, "blue_1").value,
            Gene.GetGene(appearance, "red_2").value, Gene.GetGene(appearance, "green_2").value, Gene.GetGene(appearance, "blue_2").value};

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (sprite[i, j] == 1) texture.SetPixel(i, j, new Color(colors[0]/255, colors[1]/255, colors[2]/255));
                else if (sprite[i, j] == 2) texture.SetPixel(i, j, new Color(colors[3]/255, colors[4]/255, colors[5]/255));
                else if (sprite[i, j] == 3) texture.SetPixel(i, j, new Color(0, 0, 0));
                else if (sprite[i, j] == 4) texture.SetPixel(i, j, new Color(1, 1, 1));
                else texture.SetPixel(i, j, new Color(1, 1, 1, 0));
            }
        }


        texture.Apply();
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        return texture;
    }

    public int DetermineWidth()
    {
        int tail = Gene.GetGene(appearance, "Tail W").value;
        if (Gene.GetGene(appearance, "Tail H").value == 0) tail = 0;

        switch (Gene.GetGene(appearance, "Head Pos").value)
        {
            case 0:
                return tail + Gene.GetGene(appearance, "Body W").value + Gene.GetGene(appearance, "Head W").value;
            case 1:
                return tail + Gene.GetGene(appearance, "Body W").value;
            default:
                return tail + Gene.GetGene(appearance, "Body W").value + Gene.GetGene(appearance, "Head W").value - 2;
        }
    }

    public int DetermineHeight()
    {
        int ear = Gene.GetGene(appearance, "Ear H").value;
        if (Gene.GetGene(appearance, "Ear W").value == 0) ear = 0;

        switch (Gene.GetGene(appearance, "Head Pos").value)
        {
            case 0:
                int biggest = Gene.GetGene(appearance, "Head H").value;
                if (biggest < Gene.GetGene(appearance, "Body H").value) biggest = Gene.GetGene(appearance, "Body H").value;
                return ear + Gene.GetGene(appearance, "Paws H").value + biggest;
            case 1:
                return ear + Gene.GetGene(appearance, "Body H").value + Gene.GetGene(appearance, "Paws H").value + Gene.GetGene(appearance, "Head H").value;
            default:
                return ear + Gene.GetGene(appearance, "Body H").value + Gene.GetGene(appearance, "Paws H").value + Gene.GetGene(appearance, "Head H").value - 2;
        }
    }

    public override void generateGenome(System.Random prng)
    {
        year = ProceduralIsland.instance.GetComponent<TimeManagement>().actual_year;

        //Appearance
        appearance.Add(new Gene("Ear W", 2, 2, true, prng));
        appearance.Add(new Gene("Ear H", 0, 3, true, prng));
        appearance.Add(new Gene("Ear Type", 0, 6, true, prng));
        appearance.Add(new Gene("Tail W", 0, 3, true, prng));
        appearance.Add(new Gene("Tail H", 0, 3, true, prng));
        appearance.Add(new Gene("Body W", 8, 14, true, prng));
        appearance.Add(new Gene("Body H", 4, 7, true, prng));
        appearance.Add(new Gene("Body Type", 0, 3, true, prng));
        appearance.Add(new Gene("Paws W", 1, (int)(Gene.GetGene(appearance, "Body W").value / 6), true, prng));
        appearance.Add(new Gene("Paws H", 1, 4, true, prng));
        appearance.Add(new Gene("Paws Type", 0, 1, true, prng));
        appearance.Add(new Gene("Head W", 5, 8, true, prng));
        int headH = (int)Gene.GetGene(appearance, "Body H").value + (int)Gene.GetGene(appearance, "Paws H").value;
        if (headH >= 8) headH = 8;
        appearance.Add(new Gene("Head H", 5, headH, true, prng));
        appearance.Add(new Gene("Head Type", 0, 6, true, prng));
        appearance.Add(new Gene("Head Pos", 0, 2, true, prng));

        appearance.Add(new Gene("red_1", 0, 255, true, prng));
        appearance.Add(new Gene("green_1", 0, 255, true, prng));
        appearance.Add(new Gene("blue_1", 0, 255, true, prng));

        appearance.Add(new Gene("red_2", 0, 255, true, prng));
        appearance.Add(new Gene("green_2", 0, 255, true, prng));
        appearance.Add(new Gene("blue_2", 0, 255, true, prng));

        //Composition
        composition.Add(new Gene("Syllable Number", 2, 4, true, prng));
        composition.Add(new Gene("Syllable 0", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 1", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 2", 0, 19, true, prng));
        composition.Add(new Gene("Syllable 3", 0, 19, true, prng));

        weight = DetermineHeight() * DetermineWidth();

        int weight_influence = weight / 50;

        composition.Add(new Gene("Thirst", 5, 20, true, prng));
        water = Gene.GetGene(composition, "Thirst").value - weight_influence;
        currWater = water;
        composition.Add(new Gene("Hunger", 5, 20, true, prng));
        food = Gene.GetGene(composition, "Hunger").value - weight_influence;
        currFood = food;
        composition.Add(new Gene("Life", 1, 5, true, prng));
        life = Gene.GetGene(composition, "Life").value + weight_influence;
        currLife = life;
        composition.Add(new Gene("Breath", 0, 10, true, prng));
        breath = Gene.GetGene(composition, "Breath").value + weight_influence;
        composition.Add(new Gene("Speed", 100, 200, true, prng));
        speed = (float)(Gene.GetGene(composition, "Speed").value)/weight;
        composition.Add(new Gene("Death", 10, 200, true, prng));
        //Behavior
        behavior.Add(new Gene("Bravery", 0, 1, true, prng)); // Check if the animal try to leak or to attack
    }
}
