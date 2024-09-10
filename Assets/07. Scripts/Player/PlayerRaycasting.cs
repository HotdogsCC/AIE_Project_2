using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    [SerializeField] private float castDistance = 3f;
    [SerializeField] private LayerMask lookMask;
    [SerializeField] private GameObject interactPopUp;
    private Transform cam;
    private Interactable previousInteractable = null;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, castDistance, lookMask))
        {
            //make interactable outline
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            previousInteractable = interactable;

            if (interactable != null)
            {
                interactable.LookedAt();
                interactPopUp.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        else if(previousInteractable != null)
        {
            previousInteractable.LookedAway();
            interactPopUp.SetActive(false);
            previousInteractable = null;
        }
    }
}
