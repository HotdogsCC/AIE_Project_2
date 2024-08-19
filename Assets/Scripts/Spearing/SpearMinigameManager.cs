using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject spearMinigame;

    private Transform fish;
    // Start is called before the first frame update
    public void StartMinigame(Transform inFish, Transform spearTip)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        fish = inFish;
        fish.GetComponent<FishMovement>().enabled = false;
        fish.SetParent(spearTip);
        fish.localPosition = Vector3.zero;
        spearMinigame.SetActive(true);
        FindObjectOfType<FirstPersonController>().enabled = false;
    }

    public void EndMinigame(bool gameWin)
    {
        if(gameWin)
        {
            Debug.Log("game won, fish (will be) stashed in inventory");
            Destroy(fish.gameObject);
        }
        else
        {
            Debug.Log("game lost, fish back in wild, probably should damage the player and swim off");
            fish.SetParent(null);
            fish.GetComponent<FishMovement>().enabled = true;
            fish.GetComponent<FishMovement>().CalculateNewDirection();

        }

        spearMinigame.SetActive(false);
        FindObjectOfType<FirstPersonController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
