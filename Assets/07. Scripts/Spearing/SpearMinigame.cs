using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpearMinigame : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private SpearMinigameManager manager;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Transform bar;
    [SerializeField] private Transform fishImage;

    [Header("Gameplay Factors")]
    [SerializeField] private float barHealth = 10f;
    [SerializeField] private float barDeclineSpeed = 3f;
    [SerializeField] private float barSpeedIncreaseFactor = 1.25f;
    [SerializeField] private int numToWin = 10;
    [SerializeField] private float timeInCircle = 1f;

    [Header("Graphics Factors")]
    [SerializeField] private float mouseShakeFactor = 20f;
    [SerializeField] private float fishShakeFactor = 5f;


    private float _barHealth = 10f;
    private float _barDeclineSpeed = 10f;
    private int _numToWin = 10;
    private float _timeInCircle = 1f;

    private bool running = false;
    private bool runningRight = true;
    private bool returning = false;

    private bool isGameRunning;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SpearMinigameManager>();
        StartCoroutine(StartRunning(3f));
    }

    private void OnEnable()
    {
        _barHealth = barHealth;
        _barDeclineSpeed = barDeclineSpeed;
        _numToWin = numToWin;
        _timeInCircle = timeInCircle;
    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.position = Input.mousePosition;

        Vector2 vectorFromOrigin = new Vector2(cursor.transform.localPosition.x, cursor.transform.localPosition.y);

        if (running)
        {
            RunOffScreen();
            if (runningRight)
            {
                if (Input.mousePosition.x == Screen.width - 1)
                {
                    StopRunning();
                }
            }
            else
            {
                if (Input.mousePosition.x == 0)
                {
                    StopRunning();
                }
            }
        }
        else if (returning)
        {
            LerpToPos();
            t -= Time.deltaTime;
            if (t < 0)
            {
                t = 0;
                returning = false;
            }
        }
        else
        {
            ShakeFishImage();

            //runs when mouse is in range
            if (vectorFromOrigin.magnitude < 40)
            {
                _timeInCircle -= Time.deltaTime;
                _barHealth += Time.deltaTime * _barDeclineSpeed / 2;
                bar.localPosition = new Vector2(Random.Range(-5, 6), Random.Range(-442, -431)); //Shakes bar

                //shakes the mouse
                Vector3 movementVector = new Vector3(Random.Range(-1 * mouseShakeFactor * Screen.width / 1920, mouseShakeFactor * Screen.width / 1920),
                    Random.Range(-1 * mouseShakeFactor * Screen.height / 1080, mouseShakeFactor * Screen.height / 1080), 0);
                Mouse.current.WarpCursorPosition(cursor.transform.position + movementVector);

                if (_timeInCircle < 0)
                {
                    //Moves mouse away from centre
                    Mouse.current.WarpCursorPosition(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
                    _barHealth = barHealth;
                    _numToWin--;
                    _barDeclineSpeed *= barSpeedIncreaseFactor;
                    _timeInCircle = timeInCircle;
                    if (_numToWin <= 0)
                    {
                        manager.EndMinigame(true);
                    }
                }


            }
            else
            {
                _barHealth -= Time.deltaTime * _barDeclineSpeed;
                bar.localPosition = new Vector2(0, -436);

                if (_barHealth <= 0)
                {
                    manager.EndMinigame(false);
                }
            }

            bar.localScale = new Vector3((_barHealth / barHealth * 10), 0.1f, 1f);
            bar.GetComponent<Image>().color = new Color(1 - _barHealth / barHealth, _barHealth / barHealth, 0);
        }



    }

    private void RunOffScreen()
    {
        t += Time.deltaTime;
        LerpToPos();
        if (t > 1)
        {
            t = 0;
            running = false;
            returning = false;
            manager.EndMinigame(false);
        }

    }

    private void StopRunning()
    {
        running = false;
        returning = true;
    }

    private void ShakeFishImage()
    {
        fishImage.localPosition = new Vector2(Random.Range(-fishShakeFactor, fishShakeFactor),
            Random.Range(77.5f - fishShakeFactor, 77.5f + fishShakeFactor));
    }

    private void LerpToPos()
    {
        if (runningRight)
        {
            fishImage.localPosition = new Vector2((1f - Mathf.Pow(1 - t, 5)) * 1600f, 77.5f);
        }
        else
        {
            fishImage.localPosition = new Vector2((1f - Mathf.Pow(1 - t, 5)) * -1600f, 77.5f);
        }
    }

    private IEnumerator StartRunning(float time2Wait)
    {
        yield return new WaitForSeconds(time2Wait);
        if(!running && !returning)
        {
            running = true;
            if (Random.Range(0, 2) == 0)
            {
                runningRight = true;
            }
            else
            {
                runningRight = false;
            }
        }

        StartCoroutine(StartRunning(3f));
    }

}
