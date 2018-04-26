using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NameGenerator
{


    public static string GenerateName(List<Gene> composition)
    {
        string[] syllable = { "ra", "ja", "za", "kar", "ro", "ma", "dy", "mar", "lex", "lo",
            "be", "mi", "su", "lu", "sau", "pi", "rex", "zor", "bla", "dur"};
        string name = "";

        for(int i = 0; i < Gene.GetGene(composition, "Syllable Number").value; i++)
        {
            name += syllable[Gene.GetGene(composition, "Syllable " + i).value];
        }

        return char.ToUpper(name[0]) + name.Substring(1); ;
    }


}
