using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public Entity selected, prevSelected;
    public GameObject panel;
    public Button close;
    public ScrollRect scroll;
    public GeneList genes;
    public Dropdown gene_choice;
    public Text infos;
    public Text year;
    public Image o2Bar;
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
        Bar();
    }

    void Window(){
        if (selected == null){
            panel.SetActive(false);
        }
        else{
            panel.SetActive(true);
            infos.text = "Name: " + selected.name + "\n" ;
            if(selected.GetComponent<MonoBehaviour>().GetType().Name == "Animal")
            {
                Animal a = (Animal)selected;
                infos.text += "Life: " + a.currLife + " / " + a.life + "\n";
                infos.text += "Food: " + a.currFood + " / " + a.food + "\n";
                infos.text += "Water: " + a.currWater + " / " + a.water + "\n";
                infos.text += "Breath: " + a.breath + "\n";
                infos.text += "Speed: " + a.speed + "\n";
                infos.text += "Weight: " + a.weight + "\n";
                infos.text += "Age: " + a.age + "\n";
            }
            else if (selected.GetComponent<MonoBehaviour>().GetType().Name == "Flower")
            {
                Flower f = (Flower)selected;
                infos.text += "Life: " + f.currLife + " / " + f.life + "\n";
                infos.text += "Age: " + f.age + "\n";
            }

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
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(target.x, target.y), Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Flower"));
            if (hit || hit2) {
                prevSelected = selected;
                if (hit) selected = hit.transform.gameObject.GetComponent<Animal>();
                else selected = hit2.transform.gameObject.GetComponent<Flower>();
                
            }
        }
    }

    void Bar()
    {
        year.text = "Year: " + ProceduralIsland.instance.GetComponent<TimeManagement>().actual_year.ToString();
        float o2 = ProceduralIsland.instance.GetComponent<Atmosphere>().oxygene;
        float co2 = ProceduralIsland.instance.GetComponent<Atmosphere>().co2;
        o2Bar.fillAmount = o2 / (o2 + co2);
    }

    public void Onclick(){
        selected = null;
    }
}
