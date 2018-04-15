﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    public List<GameObject> animals = new List<GameObject>();
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
                animal.transform.parent = pools.transform;
        
                animals.Add(animal);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}