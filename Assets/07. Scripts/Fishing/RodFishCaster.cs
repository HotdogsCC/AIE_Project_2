using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RodFishCaster : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float upForce;
    [SerializeField] private float chargeSpeed = 5f;
    [SerializeField] private Transform topOfFishingLine;
    [SerializeField] private Transform endOfFishingLinePos;
    [SerializeField] private Transform buoyGO;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject buoy;
    private bool isCast = false;
    private float chargeForce = 0;


    // Update is called once per frame
    void Update()
    {
        //On left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (isCast)
            {
                FindObjectOfType<FishRodMinigameManager>().EndMinigame();
                UnsetCast();
            }
            else
            {
                chargeForce = 0;
            }
        }

        if (Input.GetMouseButton(0))
        {
            chargeForce += chargeSpeed * Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (!isCast)
            {
                CastLine();
            }
            else
            {
                isCast = false;
            }
            
        }
    }


    private void CastLine()
    {
        //makes it so the buoy has gravity and chucks a force on it
        isCast = true;
        buoy.GetComponent<Buoyancy>().enabled = true;
        buoy.GetComponentInChildren<SphereCollider>().enabled = true;
        buoy.transform.SetParent(null);
        buoy.transform.rotation = Quaternion.Euler(Vector3.zero);
        Rigidbody rb = buoy.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(Camera.main.transform.forward * (throwForce + chargeForce));
        rb.AddForce(0, upForce + chargeForce, 0);
    }

    public void ReelLine()
    {
        buoy.GetComponent<Buoyancy>().enabled = false;
        buoy.GetComponentInChildren<SphereCollider>().enabled = false;
        buoy.GetComponent<Animator>().SetBool("inWater", false);
        FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
        foreach (var fish in fishies)
        {
            //fish.GetComponent<SphereCollider>().enabled = false;
            fish.GetComponentInParent<FishMovement>().IDontSeeRod();
            fish.BuoyOutWater();
        }
        buoy.transform.SetParent(endOfFishingLinePos);
        buoy.transform.localPosition = Vector3.zero;
        Rigidbody rb = buoy.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        buoy.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetCast()
    {
        isCast = false;
    }

    public void UnsetCast()
    {
        isCast = true;
    }

    //Makes it so the fishing line is drawn right before the frame is set to render, that way it looks good!
    private void OnEnable()
    {
        Application.onBeforeRender += DrawTheFishingLine;
    }

    //unsubs so unity stops spitting errors
    private void OnDisable()
    {
        Application.onBeforeRender -= DrawTheFishingLine;
    }

    private void DrawTheFishingLine()
    {
        lineRenderer.SetPosition(0, topOfFishingLine.position);
        lineRenderer.SetPosition(1, buoyGO.position);
    }
}
