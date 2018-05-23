using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneList : MonoBehaviour {
    public Text patern;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void printList(List<Gene> l)
    {
        Text newText;
        newText = Instantiate(patern, transform);
        newText.text = "Name";
        newText.fontStyle = FontStyle.Bold;
        newText.alignment = TextAnchor.MiddleCenter;
        newText = Instantiate(patern, transform);
        newText.text = "Value";
        newText.fontStyle = FontStyle.Bold;
        newText.alignment = TextAnchor.MiddleCenter;
        newText = Instantiate(patern, transform);
        newText.text = "Mutable?";
        newText.fontStyle = FontStyle.Bold;
        newText.alignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < l.Count; i++)
        {
            newText = Instantiate(patern, transform);
            newText.text = l[i].name;
            newText.alignment = TextAnchor.MiddleCenter;
            newText = Instantiate(patern, transform);
            newText.text = l[i].value.ToString();
            newText.alignment = TextAnchor.MiddleCenter;
            newText = Instantiate(patern, transform);
            if (l[i].mutable == true) newText.text = "Yes";
            else newText.text = "No";
            newText.alignment = TextAnchor.MiddleCenter;
        }
    }

    public void DestroyList(){
        Transform transform;
        transform = GetComponent<Transform>();
        foreach (Transform child in transform)
        {
                Destroy(child.gameObject);
        }
    }

    
}
