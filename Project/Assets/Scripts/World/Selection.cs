using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public Animal selected, prevSelected;
    public GameObject canvas;
    public Button close;
    public ScrollRect scroll;
    public GeneList genes;
    public Dropdown gene_choice;
    public Text infos;
    private Vector3 target;
    private string disp_selected;

	// Use this for initialization
	void Start () {
        selected = null;
        disp_selected = null;
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
            infos.text = "Name: " + selected.name + "\n" ;
            infos.text += "Life: " + selected.currLife + " / " + selected.life + "\n";
            infos.text += "Food: " + selected.currFood + " / " + selected.food + "\n";
            infos.text += "Water: " + selected.currWater + " / " + selected.water + "\n";
            infos.text += "Breath: " + selected.breath + "\n";
            infos.text += "Speed: " + selected.speed + "\n";
            infos.text += "Weight: " + selected.weight + "\n";
            genes = scroll.content.GetComponent<GeneList>();
            if (selected != prevSelected){
                disp_selected = null;
                prevSelected = selected;
            }
            if (gene_choice.captionText.text == "Appearance" && disp_selected != "Appearance"){
                genes.DestroyList();
                genes.printList(selected.appearance);
                disp_selected = "Appearance";
            }
            else if (gene_choice.captionText.text == "Behaviour" && disp_selected != "Behaviour"){
                genes.DestroyList();
                genes.printList(selected.behavior);
                disp_selected = "Behaviour";
            }
            else if (gene_choice.captionText.text == "Composition" && disp_selected != "Composition"){
                genes.DestroyList();
                genes.printList(selected.composition);
                disp_selected = "Composition";
            }
            else if (gene_choice.captionText.text == "Show Genes"){
                genes.DestroyList();
                disp_selected = null;
            }
        }
    }

    void Select(){
        if (Input.GetMouseButtonDown(0)){
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(target.x, target.y), Vector2.zero, 0f, 1<<LayerMask.NameToLayer("Entity"));
            if (hit) {
                prevSelected = selected;
                selected = hit.transform.gameObject.GetComponent<Animal>();
            }
        }
    }

    public void Onclick(){
        selected = null;
    }
}
