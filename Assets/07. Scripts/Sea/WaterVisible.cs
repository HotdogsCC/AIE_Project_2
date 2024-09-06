using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class WaterVisible : MonoBehaviour
{
    private Water water;

    private void Start()
    {
        water = GetComponent<Water>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            water.enabled = false;
        }
    }
}
