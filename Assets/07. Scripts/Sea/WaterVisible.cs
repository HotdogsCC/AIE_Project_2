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
            water.enabled = true;
            Debug.Log("enabaled");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            water.enabled = false;
            Debug.Log("disabled");
        }
    }
}
