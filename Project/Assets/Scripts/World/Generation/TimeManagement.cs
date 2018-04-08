using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{


    
    public int actual_year = 0;
    public int actual_frame = 0;
    int frame_limit = 200;
    // Use this for initialization
    void Start()
    {
        actual_frame = 0;
        actual_year = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (actual_frame >= frame_limit)
        {
            actual_frame = 0;
            actual_year++;
        }
        actual_frame++;
    }
}
