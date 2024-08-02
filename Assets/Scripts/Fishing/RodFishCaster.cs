using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodFishCaster : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float upForce;
    [SerializeField] private Transform topOfFishingLine;
    [SerializeField] private Transform endOfFishingLine;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject buoy;


    // Update is called once per frame
    void Update()
    {
        //On left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CastLine();
        }
    }


    private void CastLine()
    {
        //makes it so the buoy has gravity and chucks a force on it
        buoy.transform.SetParent(null);
        Rigidbody rb = buoy.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(Camera.main.transform.forward * throwForce);
        rb.AddForce(0, upForce, 0);
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
        lineRenderer.SetPosition(1, endOfFishingLine.position);
    }
}
