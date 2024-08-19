using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpearMinigame : MonoBehaviour
{
    [SerializeField] private SpearMinigameManager manager;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Transform bar;

    [SerializeField] private float barHealth = 10f;
    [SerializeField] private float barDeclineSpeed = 3f;
    [SerializeField] private float barSpeedIncreaseFactor = 1.25f;
    [SerializeField] private int numToWin = 10;

    private float _barHealth = 10f;
    private float _barDeclineSpeed = 10f;
    private int _numToWin = 10;

    private bool isGameRunning;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SpearMinigameManager>();
    }

    private void OnEnable()
    {
        _barHealth = barHealth;
        _barDeclineSpeed = barDeclineSpeed;
        _numToWin = numToWin + 1;
    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.position = Input.mousePosition;

        Vector2 vectorFromOrigin = new Vector2(cursor.transform.localPosition.x, cursor.transform.localPosition.y);
        if (vectorFromOrigin.magnitude < 40)
        {
            Mouse.current.WarpCursorPosition(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
            _barHealth = barHealth;
            _numToWin--;
            _barDeclineSpeed *= barSpeedIncreaseFactor;
            if(_numToWin <= 0)
            {
                Debug.Log("game win");
                manager.EndMinigame(true);
            }
        }
        else
        {
            _barHealth -= Time.deltaTime * _barDeclineSpeed;

            if(_barHealth <= 0)
            {
                Debug.Log("gamelose");
                manager.EndMinigame(false);
            }
        }

        bar.localScale = new Vector3 ((_barHealth / barHealth * 10), 0.1f, 1f);
        bar.GetComponent<Image>().color = new Color(1 - _barHealth / barHealth, _barHealth/barHealth, 0);

    }

}
