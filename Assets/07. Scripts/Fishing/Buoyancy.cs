using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private Rigidbody rb;
    private FishRodMinigameManager manager;
    private Animator animator;
    private float waterYLevel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.FindObjectOfType<FishRodMinigameManager>();
        animator = GetComponent<Animator>();
        waterYLevel = FindObjectOfType<GroundWaterManager>().GetYLevel();
    }

    //makes the buoy "float" on the water by removing gravity and velocity
    private void Update()
    {
        if(transform.position.y < waterYLevel)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            animator.SetBool("inWater", true);
            FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
            foreach (var fish in fishies)
            {
                fish.BuoyInWater(transform.position);
                fish.GetComponentInParent<BoxCollider>().enabled = true;
            }
        }
    }

}
