using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notebook : Interactable
{
    [SerializeField] TextMeshProUGUI text;
    private bool opened = false;


    public override void Interact()
    {
        RodFishCaster rod = FindObjectOfType<RodFishCaster>();
        rod.ReelLine();
        rod.enabled = false;
        GameObject.FindGameObjectWithTag("mainCinemachine").GetComponent<CinemachineVirtualCamera>().enabled = false;
        FindFirstObjectByType<FirstPersonController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GameObject.FindGameObjectWithTag("ItemCamera").GetComponent<Camera>().enabled = false;
        StartCoroutine(OneSecondBuffer());
    }

    private void Update()
    {
        text.text = 
            $"Normal fish caught: {Stats.GetNormalFishCaught()}\n" +
            $"Babarusa caught : {Stats.GetBabarusaFishCaught()}";

        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(opened)
            {
                GameObject.FindGameObjectWithTag("mainCinemachine").GetComponent<CinemachineVirtualCamera>().enabled = true;
                FindFirstObjectByType<FirstPersonController>().enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                GameObject.FindGameObjectWithTag("ItemCamera").GetComponent<Camera>().enabled = true;
                opened = false;
                RodFishCaster rod = FindObjectOfType<RodFishCaster>();
                rod.enabled = true;
            }
        }
    }

    private IEnumerator OneSecondBuffer()
    {
        yield return new WaitForSeconds(1);
        opened = true;
    }
}
