using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour {


    public int oxygene;
    public int co2;
    public int windSpeed;
    public int sunshine;
    public int temperature;

    private int minOxygene = 0;
    private int minCo2 = 0;
    private int minWindSpeed = 0;
    private int maxWindSpeed = 150;
    private int minSunshine = 0;
    private int maxSunshine = 100;


    // Use this for initialization
    public void Awake () {
        System.Random prng = new System.Random(GetComponent<ProceduralIsland>().seed);
        oxygene = prng.Next(10000, 15000);
        co2 = prng.Next(1000, 2000);
        windSpeed = prng.Next(20, 50);
        sunshine = prng.Next(50, 100);
        temperature = prng.Next(-5, 25) + (int)(10 * 1/sunshine);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
