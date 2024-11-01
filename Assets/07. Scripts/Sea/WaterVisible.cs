using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class WaterVisible : MonoBehaviour
{
    [SerializeField] private GameObject water;

    private void Start()
    {
        if (water == null)
        {
            Debug.Log($"UHHHHH, you forgot to add a reference to the water group for {transform.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" && other.transform.name == "PlayerCapsule")
        {
            water.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && other.transform.name == "PlayerCapsule")
        {
            water.SetActive(false);
        }
    }
}
