using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Selection>().selected == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(GetComponent<Selection>().selected.tag);
            this.gameObject.SetActive(true);
        }
    }
}
