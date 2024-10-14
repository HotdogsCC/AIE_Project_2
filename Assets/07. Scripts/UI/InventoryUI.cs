using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool opened = false;
    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI normalFishText;
    [SerializeField] private TextMeshProUGUI babarusaFishText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (opened)
            {
                opened = false;
                Debug.Log("closed inventory");
                Pausing.Resume();
                ui.SetActive(false);
            }
            else
            {
                opened = true;
                Debug.Log("opened inventory");
                Pausing.Freeze();
                ui.SetActive(true);
                normalFishText.text = "normal fish: " + Stats.GetNormalFishInv().ToString();
                babarusaFishText.text = "babrusa: " + Stats.GetBabarusaFishInv().ToString();
            }    
        }
    }
}
