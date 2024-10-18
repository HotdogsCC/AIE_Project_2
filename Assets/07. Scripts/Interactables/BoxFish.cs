using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFish : Interactable
{
    public override void Interact()
    {
        Debug.Log("I just collected a box fish");

        FindObjectOfType<Lantern>().FillUpLamp();

        Destroy(gameObject.GetComponentInParent<FishMovement>().gameObject);
    }
}
