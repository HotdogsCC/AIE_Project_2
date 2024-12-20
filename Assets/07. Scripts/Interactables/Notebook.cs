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
        if(Input.GetKeyDown(KeyCode.Backslash))
        {
            for(int i = 0; i < 8; i++)
            {
                Stats.FishCaught(i);
            }
        }

        text.text = 
            $"Normal fish caught: {Stats.GetFishCaught(0)}\n" +
            $"Babarusa caught: {Stats.GetFishCaught(1)}\n" +
            $"Face fish caught: {Stats.GetFishCaught(2)}\n" +
            $"Coral fish caught: {Stats.GetFishCaught(3)}\n" +
            $"Box fish caught: {Stats.GetFishCaught(4)}\n" +
            $"Lampery fish caught: {Stats.GetFishCaught(5)}\n" +
            $"Eyeless fish caught: {Stats.GetFishCaught(6)}\n" +
            $"Lure jaw fish caught: {Stats.GetFishCaught(7)}";

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
