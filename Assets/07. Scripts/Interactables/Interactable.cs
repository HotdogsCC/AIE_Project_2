using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public abstract void Interact();

    public void LookedAt()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 3;
        }
        else
        {
            Debug.LogError($"Interactable object {gameObject.name} must have an Outline script.");
        }
    }

    public void LookedAway()
    {
        if (outline != null)
        {
            outline.OutlineWidth = 0;
        }
        else
        {
            Debug.LogError($"Interactable object {gameObject.name} must have an Outline script.");
        }
    }
}
