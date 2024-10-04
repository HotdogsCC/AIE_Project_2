using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class Head : MonoBehaviour
{
    public bool inWater = false;
    private bool overlap = false;
    private float waterYLevel = 0f;

    private void Start()
    {
        waterYLevel = FindObjectOfType<GroundWaterManager>().GetYLevel();
    }

    private void Update()
    {
        if (transform.position.y < waterYLevel)
        {
            if (!inWater)
            {
                inWater = true;
            }
        }
        else
        {
            if (inWater)
            {
                inWater = false;
            }
        }
    }
}
