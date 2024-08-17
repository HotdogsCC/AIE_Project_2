using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject spearMinigame;
    // Start is called before the first frame update
    public void StartMinigame()
    {
        spearMinigame.SetActive(true);
        FindObjectOfType<FirstPersonController>().enabled = false;
    }

    public void EndMinigame()
    {
        spearMinigame.SetActive(false);
        FindObjectOfType<FirstPersonController>().enabled = true;
    }
}
