using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishIconMover : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Slider slider;

    [Header("Adjustable Numbers")]
    [SerializeField] private float moveSpeed = 1f;
    [Range(0, 100)]
    [SerializeField] private int chanceOfChangingDirection = 10;

    [Header("Non Adjustable Number")]
    [SerializeField] private bool movingUp = true;

    // Update is called once per frame
    void Update()
    {

        if (movingUp)
        {
            slider.value += moveSpeed * Time.deltaTime;
        }
        else
        {
            slider.value -= moveSpeed * Time.deltaTime;
        }

        if(slider.value <= 0)
        {
            slider.value = 1;
            movingUp = true;
        }
        if (slider.value >= 100)
        {
            slider.value = 99;
            movingUp = false;
        }
    }

    private void FixedUpdate()
    {
        if (chanceOfChangingDirection > Random.Range(0, 100))
        {
            movingUp = !movingUp;
        }
    }

    public void SetParameters(float _moveSpeed, int _chanceOfMoveDirection)
    {
        moveSpeed = _moveSpeed;
        chanceOfChangingDirection = _chanceOfMoveDirection;
    }
}
