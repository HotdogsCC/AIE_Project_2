using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private Rigidbody rb;
    private FishRodMinigameManager manager;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.FindObjectOfType<FishRodMinigameManager>();
        animator = GetComponent<Animator>();    
    }

    //makes the buoy "float" on the water by removing gravity and velocity
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "water")
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            animator.SetBool("inWater", true);
            FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
            foreach (var fish in fishies)
            {
                fish.GetComponent<SphereCollider>().enabled = true;
                fish.GetComponentInParent<BoxCollider>().enabled = true;
            }
            //manager.StartMinigame();
        }
    }

}
