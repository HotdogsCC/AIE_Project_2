using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private Rigidbody rb;
    private FishRodMinigameManager manager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.FindObjectOfType<FishRodMinigameManager>();
    }

    //makes the buoy "float" on the water by removing gravity and velocity
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            Debug.Log("in the water");
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            manager.StartMinigame();
        }
    }
}
