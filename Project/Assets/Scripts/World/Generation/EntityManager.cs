﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    public List<GameObject> animals = new List<GameObject>();
    public List<GameObject> flowers = new List<GameObject>();

    public GameObject pools;

    public int groupe = 10;

    
    public void GenerateAnimals()
    {
        for(int grp = 0; grp < groupe; grp++)
        {
            int xGroup;
            int yGroup;
            int seed = Random.Range(-10000, 10000);

            do
            {
                xGroup = Random.Range(-100, 100);
                yGroup = Random.Range(-100, 100);
            } while (ProceduralIsland.instance.map.GetTile(new Vector3Int(xGroup, yGroup, 0)) != ProceduralIsland.instance.tiles[0]);

            for (int a = 0; a < Random.Range(3, 6); a++)
            {
                GameObject animal = new GameObject();
                Animal script = animal.AddComponent<Animal>();

                int x;
                int y;
                do
                {
                    x = Random.Range(-5, 5);
                    y = Random.Range(-5, 5);
                } while (ProceduralIsland.instance.map.GetTile(new Vector3Int(x + xGroup, y + yGroup, 0)) != ProceduralIsland.instance.tiles[0] && ProceduralIsland.instance.map.GetTile(new Vector3Int(x + xGroup, y + yGroup, 0)) != ProceduralIsland.instance.tiles[1]);

                script.coord = new Vector2(x + xGroup + .5f, y + yGroup + .5f);
                script.seed = seed;

                script.GenerateGenome(new System.Random(script.seed));
                script.Generate();

                animal.transform.parent = pools.transform;
                animals.Add(animal);
            }
        }
    }
 

    public void GenerateFlowers()
    {
        for (int grp = 0; grp < groupe * 4; grp++)
        {
            int xGroup;
            int yGroup;
            int seed = Random.Range(-10000, 10000);

            do
            {
                xGroup = Random.Range(-100, 100);
                yGroup = Random.Range(-100, 100);
            } while (ProceduralIsland.instance.map.GetTile(new Vector3Int(xGroup, yGroup, 0)) != ProceduralIsland.instance.tiles[0]);

            for (int a = 0; a < Random.Range(2, 10); a++)
            {
                GameObject flower = new GameObject();
                Flower script = flower.AddComponent<Flower>();

                int x;
                int y;
                do
                {
                    x = Random.Range(-20, 20);
                    y = Random.Range(-20, 20);
                } while (ProceduralIsland.instance.map.GetTile(new Vector3Int(x + xGroup, y + yGroup, 0)) != ProceduralIsland.instance.tiles[0] && ProceduralIsland.instance.map.GetTile(new Vector3Int(x + xGroup, y + yGroup, 0)) != ProceduralIsland.instance.tiles[1]);

                script.coord = new Vector2(x + xGroup + .5f, y + yGroup + .5f);
                script.seed = seed;

                script.GenerateGenome(new System.Random(script.seed));
                script.GenerateFlower();

                flower.transform.parent = pools.transform;
                flowers.Add(flower);
            }
        }
    }
       
    /*
    public void GenerateFlowers()
    {
        GameObject flower = new GameObject();
        Flower script = flower.AddComponent<Flower>();
        script.coord = new Vector2(0 + .5f, 0 + .5f);
        script.seed = 56456;

        script.GenerateGenome(new System.Random(script.seed));
        script.GenerateFlower();

        flower.transform.parent = pools.transform;
        flowers.Add(flower);

    }

    public void GenerateAnimals()
      {
        GameObject animal = new GameObject();
        Animal script = animal.AddComponent<Animal>();
        script.coord = new Vector2(0 + .5f, 0 + .5f);
        script.seed = 1;

        script.GenerateGenome(new System.Random(script.seed));
        script.Generate();

        animal.transform.parent = pools.transform;
        animals.Add(animal);

        GameObject animal_2 = new GameObject();
        Animal script_2 = animal_2.AddComponent<Animal>();
        script_2.coord = new Vector2(0 + .5f, 0 + .5f);
        script_2.seed = 1;

        script_2.GenerateGenome(new System.Random(script_2.seed));
        script_2.Generate();

        animal_2.transform.parent = pools.transform;
        animals.Add(animal_2);

      }*/
    

    public void AddAnimal(GameObject animal)
    {
        animal.transform.parent = pools.transform;
        animals.Add(animal);
    }

    public void AddFlower(GameObject flower)
    {
        flower.transform.parent = pools.transform;
        flowers.Add(flower);
    }

    // Update is called once per frame
    void Update () {

      }
}
