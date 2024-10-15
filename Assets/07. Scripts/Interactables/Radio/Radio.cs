using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    [SerializeField] private GameObject radioUI;
    private bool opened = false;

    public override void Interact()
    {
        Debug.Log("opened radio");
        radioUI.SetActive(true);
        Pausing.FreezePlayer();
        RodFishCaster rod = FindObjectOfType<RodFishCaster>();
        GetComponent<DialogueRadio>().MainMenu();
        rod.enabled = false;
        opened = true; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && opened)
        {
            QuitRadio();
        }
    }

    public void QuitRadio()
    {
        Debug.Log("closed radio");
        radioUI.SetActive(false);
        Pausing.ResumePlayer();
        Cursor.lockState = CursorLockMode.Locked;
        RodFishCaster rod = FindObjectOfType<RodFishCaster>();
        rod.enabled = true;
        opened = false;
    }
}
