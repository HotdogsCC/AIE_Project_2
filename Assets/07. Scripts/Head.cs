using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class Head : MonoBehaviour
{
    public bool inWater = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "water" && !inWater)
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            inWater = false;
        }
    }
}
