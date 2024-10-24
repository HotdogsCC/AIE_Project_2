using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FishRodMinigameManager : MonoBehaviour
{
    [Header("Adjustable factors")]
    [SerializeField] private int acceptableRange = 12;
    [Range(0f, 100f)]
    [SerializeField] private float startingFishPercentage = 30f;
    [SerializeField] private float progressDecreaseFactor = 3f;
    [SerializeField] private float progressIncreaseFactor = 1f;

    [Header("Object references")]
    [SerializeField] private GameObject ui;
    [SerializeField] FishIconMover fishIconMover;
    [SerializeField] private Slider fishHighlightSlider;
    [SerializeField] private Slider fishLocationSlider;
    [SerializeField] private Image highlighter;
    [SerializeField] private GameObject sfx;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject rClickToReel;

    private float catchProgressPercentage;
    private bool isGameRunning = false;
    private TutorialPopUp tutorial;
    private FishMovement fishy = null;
    private CompassSpinner compassSpinner;

    //REMOVE BEFORE RELEASE - debug stuff
    [SerializeField] private TextMeshProUGUI debugText;

    // Start is called before the first frame update
    void Start()
    {
        catchProgressPercentage = startingFishPercentage;
        tutorial = FindFirstObjectByType<TutorialPopUp>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
           GameLoop();
        }
    }

    private void GameLoop()
    {
        //checks that the highlight is touching the fish
        if (fishHighlightSlider.value >= fishLocationSlider.value - acceptableRange
            && fishHighlightSlider.value <= fishLocationSlider.value + acceptableRange)
        {
            highlighter.color = Color.green;
            catchProgressPercentage += progressIncreaseFactor * Time.deltaTime;
        }
        else
        {
            highlighter.color = Color.red;
            catchProgressPercentage -= progressDecreaseFactor * Time.deltaTime;
        }

        if (catchProgressPercentage >= 100)
        {
            catchProgressPercentage = 100;
            rClickToReel.SetActive(true);
            if(Input.GetMouseButtonDown(1))
            {
                FindObjectOfType<TextDisplay>().DisplayText("Fish Caught", 3f);
                Stats.FishCaught(fishy.GetFishIndex());
                EndMinigame();
            }
            
        }
        else
        {
            rClickToReel.SetActive(false);
        }

        if (catchProgressPercentage <= 0)
        {
            FindObjectOfType<TextDisplay>().DisplayText("Fish Lost", 3f);
            EndMinigame();
        }

        //REMOVE BEFORE RELEASE - debug stuff
        //int temp = (int)catchProgressPercentage;
        //debugText.text = temp.ToString();

        compassSpinner.SetRotation(-0.8f * catchProgressPercentage + 40);
        progressBar.fillAmount = catchProgressPercentage / 100f;
    }

    public void StartMinigame(float _moveSpeed, int _chanceOfMoveDirection, FishMovement _fishy)
    {
        fishy = _fishy;
        fishIconMover.SetParameters(_moveSpeed, _chanceOfMoveDirection);
        catchProgressPercentage = startingFishPercentage;
        isGameRunning = true;
        ui.SetActive(true);
        FirstPersonController player = FindObjectOfType<FirstPersonController>();
        player.PreventJump();
        player.enabled = false;
        tutorial.FishingTut();
        Instantiate(sfx);
        compassSpinner = FindObjectOfType<CompassSpinner>();
    }

    public void EndMinigame()
    {
        if(fishy != null)
        {
            Destroy(fishy.gameObject);
        }
        fishy = null;
        isGameRunning = false;
        ui.SetActive(false);
        FirstPersonController player = FindObjectOfType<FirstPersonController>();
        player.enabled = true;
        player.PreventJump();  
        FindObjectOfType<RodFishCaster>().ReelLine();
        FindObjectOfType<RodFishCaster>().SetCast();
    }
}
