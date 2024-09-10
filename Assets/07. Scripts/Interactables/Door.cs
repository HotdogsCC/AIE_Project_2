using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpened = false;
    private bool opening;
    private float t = 0;

    public override void Interact()
    {
        opening = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (opening)
        {
            if(isOpened)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void CloseDoor()
    {
        t -= Time.deltaTime;
        if (t < 0f)
        {
            t = 0;
            opening = false;
            isOpened = false;
            GetComponent<BoxCollider>().enabled = true;
        }
        CubicEase();
    }

    private void OpenDoor()
    {
        t += Time.deltaTime;
        if (t > 1f)
        {
            t = 1;
            opening = false;
            isOpened = true;
            GetComponent<BoxCollider>().enabled = true;
        }
        CubicEase();
    }

    private void CubicEase()
    {
        float ease = 1 - Mathf.Pow(1 - t, 3);
        transform.localRotation = Quaternion.Euler(0f, ease * -100f, 0f);
    }
}
