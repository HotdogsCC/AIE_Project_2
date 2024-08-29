using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHookDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hook")
        {
            GetComponentInParent<FishMovement>().ISeeRod(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "hook")
        {
            GetComponentInParent<FishMovement>().IDontSeeRod();
        }
    }
}
