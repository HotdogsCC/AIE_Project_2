using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpearMinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject spearMinigame;
    [SerializeField] private Image image;
    [SerializeField] private GameObject spearTutorialPopUp;
    private TutorialPopUp tutorial;

    private Transform fish;
    // Start is called before the first frame update
    private void Start()
    {
        tutorial = FindFirstObjectByType<TutorialPopUp>();
    }
    public void StartMinigame(Transform inFish, Transform spearTip)
    {
        if(!tutorial.HasSpearTutHappened())
        {
            spearTutorialPopUp.SetActive(true);
            fish = inFish;
            fish.SetParent(spearTip);
            fish.localPosition = Vector3.zero;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            fish.GetComponent<FishMovement>().enabled = false;
            Pausing.Freeze();
        }
        else
        {
            ActuallyStart(inFish, spearTip);
        }
    }
    public void ActuallyStart(Transform inFish, Transform spearTip)
    {
        spearTutorialPopUp.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        fish = inFish;
        Sprite sprite = fish.GetComponent<FishMovement>().image;
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        fish.GetComponent<FishMovement>().enabled = false;
        fish.SetParent(spearTip);
        fish.localPosition = Vector3.zero;
        spearMinigame.SetActive(true);
        tutorial.SpearingTut();
        Pausing.Freeze();
    }
    public void ActuallyStart()
    {
        spearTutorialPopUp.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Sprite sprite = fish.GetComponent<FishMovement>().image;
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        fish.GetComponent<FishMovement>().enabled = false;
        spearMinigame.SetActive(true);
        tutorial.SpearingTut();
        Pausing.Freeze();
    }

    public void EndMinigame(bool gameWin)
    {
        if(gameWin)
        {
            FindObjectOfType<TextDisplay>().DisplayText("Fish Caught", 3f);
            Stats.FishCaught(fish.GetComponent<FishMovement>().GetFishIndex());
            Destroy(fish.gameObject);
            
        }
        else
        {
            FindObjectOfType<TextDisplay>().DisplayText("Fish Lost", 3f);
            fish.SetParent(null);
            fish.GetComponent<FishMovement>().enabled = true;
            fish.GetComponent<FishMovement>().CalculateNewDirection();
        }
        spearMinigame.SetActive(false);
        Pausing.Resume();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
