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
    [SerializeField] private int acceptableRange = 15;
    [Range(0f, 100f)]
    [SerializeField] private float startingFishPercentage = 30f;
    [SerializeField] private float progressDecreaseFactor = 3f;
    [SerializeField] private float progressIncreaseFactor = 1f;

    [Header("Object references")]
    [SerializeField] private GameObject ui;
    [SerializeField] private Slider fishHighlightSlider;
    [SerializeField] private Slider fishLocationSlider;
    [SerializeField] private Image highlighter;

    private float catchProgressPercentage;
    private bool isGameRunning = false;

    //REMOVE BEFORE RELEASE - debug stuff
    [SerializeField] private TextMeshProUGUI debugText;

    // Start is called before the first frame update
    void Start()
    {
        catchProgressPercentage = startingFishPercentage;
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
            //you win
            Debug.Log("you win");
        }
        if (catchProgressPercentage <= 0)
        {
            //you lose
            Debug.Log("you lose");
        }

        //REMOVE BEFORE RELEASE - debug stuff
        debugText.text = catchProgressPercentage.ToString();
    }

    public void StartMinigame()
    {
        isGameRunning = true;
        ui.SetActive(true);
        FindObjectOfType<FirstPersonController>().enabled = false;
    }
}
