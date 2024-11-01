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
    private bool waterYLevelDetectMode = false;
    [SerializeField] GameObject breathSFX;

    private void Start()
    {
        waterYLevel = FindObjectOfType<GroundWaterManager>().GetYLevel();
        waterYLevelDetectMode = FindAnyObjectByType<GroundWaterManager>().GetDetectMode();
    }

    private void Update()
    {
        if(waterYLevelDetectMode)
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
                    Instantiate(breathSFX);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!waterYLevelDetectMode)
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

    }

    private void OnTriggerExit(Collider other)
    {
        if (!waterYLevelDetectMode)
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
}
