using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodFishCaster : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float upForce;
    [SerializeField] private Transform topOfFishingLine;
    [SerializeField] private Transform endOfFishingLinePos;
    [SerializeField] private Transform buoyGO;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject buoy;
    private bool isCast = false;


    // Update is called once per frame
    void Update()
    {
        //On left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (isCast)
            {
                ReelLine();
            }
            else
            {
                CastLine();
            }

            
        }
    }


    private void CastLine()
    {
        //makes it so the buoy has gravity and chucks a force on it
        isCast = true;
        buoy.transform.SetParent(null);
        buoy.transform.rotation = Quaternion.Euler(Vector3.zero);
        Rigidbody rb = buoy.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(Camera.main.transform.forward * throwForce);
        rb.AddForce(0, upForce, 0);
    }

    private void ReelLine()
    {
        isCast = false;
        buoy.transform.SetParent(endOfFishingLinePos);
        buoy.transform.localPosition = Vector3.zero;
        Rigidbody rb = buoy.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        buoy.transform.localRotation = Quaternion.Euler(Vector3.zero);
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
