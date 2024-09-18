using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    [SerializeField] private GameObject radioUI;

    public override void Interact()
    {
        Debug.Log("opened radio");
        radioUI.SetActive(true);
        Pausing.FreezePlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("closed radio");
            radioUI.SetActive(false);
            Pausing.ResumePlayer();
        }
    }
}
