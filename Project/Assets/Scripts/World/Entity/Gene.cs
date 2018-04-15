using System;
using System.Collections.Generic;
using UnityEngine;

public class Gene{

    public String name;
    public int value;
    public int min;
    public int max;
    public bool mutable;

    private float factor = .1f;

    public Gene(String name, int min, int max, bool mutable, System.Random prng)
    {
        this.name = name;
        this.min = min;
        this.max = max;
        this.mutable = mutable;
        GenerateBySeed(prng);
    }

    public Gene(String name, int value, int min, int max, bool mutable)
    {
        this.name = name;
        this.value = value;
        this.min = min;
        this.max = max;
        this.mutable = mutable;
    }

    public Gene(String name, int min, int max, bool mutable)
    {
        this.name = name;
        this.min = min;
        this.max = max;
        this.mutable = mutable;

        value = UnityEngine.Random.Range(min, max);
    }

    public void Mutate()
    {
        int density = (int)(value * factor);
        int mutation = UnityEngine.Random.Range(value - density, value + density);
        value = Mathf.Clamp(mutation, min, max);
    }

    public void GenerateBySeed(System.Random prng)
    {
        this.value = prng.Next(this.min, this.max);
    }

    public static Gene GetGene(List<Gene> list, string name)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (name.CompareTo(list[i].name) == 0) return list[i];
        }
        return null;
    }
}
