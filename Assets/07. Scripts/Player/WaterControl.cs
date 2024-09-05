using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;


public class WaterControl : MonoBehaviour
{
    public bool initiateRevive = false;

    public Water WaterRef;

    void Start()
    {
        //WaterRef = GetComponent<Water>(); // Now you can access it
    }


    private void Update()
    {

        
        //if (enabled == false)
        //{
        //    WaterRef.enabled = false;
        //}
        //if (enabled == true)
        //{
        //    WaterRef.enabled = true;
        //}

        if (initiateRevive == true)
        {
            Revive();
        }
        else
        {
            Destruction();
        }
    }


    public void Destruction()
    {
        WaterRef.enabled = false;
    }
    
    public void Revive()
    {
        WaterRef.enabled = true;
    }

}
