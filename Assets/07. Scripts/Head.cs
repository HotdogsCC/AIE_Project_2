using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class Head : MonoBehaviour
{
    public bool inWater = false;
    private bool overlap = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            if (inWater) //If you enter water and are still in water, this is an overlapping area
            {
                overlap = true;
            }
            else
            {
                inWater = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            if (overlap) //this is to stop issues of exiting water with overlapping water triggers
            {
                overlap = false;
            }
            else
            {
                inWater = false;
            }
        }
    }
}
