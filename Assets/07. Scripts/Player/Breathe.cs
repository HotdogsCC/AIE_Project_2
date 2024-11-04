using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Breathe : MonoBehaviour
{
    [SerializeField] private Image breathSlider;
    [SerializeField] private float breatheTime = 30f;
    [SerializeField] private float breatheRecoverySpeed = 15f;
    [SerializeField] private float outOfBreathDamage = 20f;
    [SerializeField] private GameObject chokingSFX;
    [SerializeField] private Volume ppProfile;
    private Vignette vig;

    private bool playSFX = true;

    private float _breathTime = 30f;

    private Head head;
    private Health health;
    private void Start()
    {
        head = FindObjectOfType<Head>();
        health = FindObjectOfType<Health>();
        _breathTime = breatheTime;
        ppProfile.profile.TryGet<Vignette>(out vig);
        vig.intensity.value = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            _breathTime = breatheTime;
        }

        if (head.inWater)
        {
            _breathTime -= Time.deltaTime;

            if (_breathTime < 0)
            {
                _breathTime = 0;
                health.ChangeHealth(-1f * outOfBreathDamage * Time.deltaTime);
                if(playSFX)
                {
                    Instantiate(chokingSFX);
                    playSFX = false;
                }
            }
        }
        else
        {
            _breathTime += Time.deltaTime * breatheRecoverySpeed;

            if (_breathTime > breatheTime)
            {
                _breathTime = breatheTime;
                playSFX = true;
            }
        }

        SetBar();
    }

    private void SetBar()
    {
        breathSlider.fillAmount = _breathTime / breatheTime;
        vig.intensity.value = Mathf.Clamp((-9f / 100f) * (_breathTime) + 1, 0.2f, 1f);
    }

    public void SetBreatheTime(float input)
    {
        breatheTime = input;
    }

}
