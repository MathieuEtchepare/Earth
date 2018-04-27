using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public Animal selected;
    public GameObject canvas;
    public Text infos;
    public BoxCollider bc;

	// Use this for initialization
	void Start () {
        selected = null;
        BoxCollider bc = (BoxCollider)gameObject.AddComponent(typeof(BoxCollider));
    }
	
	// Update is called once per frame
	void Update () {
        Select();
        Window();
}
    
    void Window(){
        if (selected == null){
            canvas.SetActive(false);
        }
        else{
            canvas.SetActive(true);
            infos.text = "Name: " + selected.name + "\n" + "Life: " + selected.life;
        }
    }

    void Select(){
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit){
                if (hitInfo.transform.gameObject.tag == "Entity"){
                    selected = hitInfo.transform.gameObject.GetComponent<Animal>();
                }
            }
            else
            {
                selected = null;
            }

        }
    }

}
