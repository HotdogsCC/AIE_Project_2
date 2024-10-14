using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] GameObject lanternObj;
    [SerializeField] private Material glowMat;
    [SerializeField] private Light lanturnLight;

    [SerializeField] private float intensSpeed = 10f;
    private Color glowColor = new Color(0.7490196f, 0.2235294f, 0f);
    private float glowIntensity = 1f;
    private float lightIntensity = 2000f;

    private bool isOn = false;

    private void Start()
    {
        UpdateLantern();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isOn = !isOn;

            if(isOn)
            {
                lanternObj.SetActive(true);
                
            }
            else
            {
                lanternObj.SetActive(false);
            }
        }

        if(isOn)
        {
            glowIntensity -= Time.deltaTime / intensSpeed;
            if(glowIntensity <= 0)
            {
                glowIntensity = 0;
            }
            UpdateLantern();
        }
    }

    private void UpdateLantern()
    {
        glowMat.color = glowColor * glowIntensity * 2;
        lanturnLight.intensity = lightIntensity * glowIntensity;
    }
}
