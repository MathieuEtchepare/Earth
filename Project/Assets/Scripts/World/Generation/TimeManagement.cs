using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    public int actual_year = 0;
    public float actual_frame = 0f;
    float frame_limit = 5f;

    void Update()
    {
        if (actual_frame >= frame_limit)
        {
            actual_frame = 0;
            actual_year++;
        }
        actual_frame += Time.deltaTime;
    }
}
