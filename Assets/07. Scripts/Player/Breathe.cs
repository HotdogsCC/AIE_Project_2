using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breathe : MonoBehaviour
{
    [SerializeField] private Image breathSlider;
    [SerializeField] private float breatheTime = 30f;
    [SerializeField] private float breatheRecoverySpeed = 15f;
    [SerializeField] private float outOfBreathDamage = 20f;

    private float _breathTime = 30f;

    private Head head;
    private Health health;
    private void Start()
    {
        head = FindObjectOfType<Head>();
        health = FindObjectOfType<Health>();
        _breathTime = breatheTime;
    }

    private void Update()
    {
        if (head.inWater)
        {
            _breathTime -= Time.deltaTime;

            if (_breathTime < 0)
            {
                _breathTime = 0;
                health.ChangeHealth(-1f * outOfBreathDamage * Time.deltaTime);
            }
        }
        else
        {
            _breathTime += Time.deltaTime * breatheRecoverySpeed;

            if (_breathTime > breatheTime)
            {
                _breathTime = breatheTime;
            }
        }

        SetBar();
    }

    private void SetBar()
    {
        breathSlider.fillAmount = _breathTime / breatheTime;
    }

}
