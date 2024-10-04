using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHookDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float radius = 20f;
    private Transform player;
    private FishMovement fishMovement;
    private Vector3 buoyPos = Vector3.zero;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>().transform;
        fishMovement = GetComponentInParent<FishMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hook")
        {
            fishMovement.ISeeRod(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "hook")
        {
            fishMovement.IDontSeeRod();
        }
    }

    private void Update()
    {
        //checks it isnt 'null'
        if(buoyPos != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, buoyPos) < radius)
            {
                fishMovement.ISeeRod(buoyPos);
            }
            else
            {
                fishMovement.IDontSeeRod();
            }
        }
    }

    public void BuoyInWater(Vector3 _buoyPos)
    {
        buoyPos = _buoyPos;
    }

    public void BuoyOutWater()
    {
        buoyPos = Vector3.zero;
    }
}
