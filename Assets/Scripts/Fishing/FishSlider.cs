using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSlider : MonoBehaviour
{ 
    [Header("Object References")]
    [SerializeField] private Slider slider;

    [Header("Adjustable Numbers")]
    [SerializeField] private float jumpStrength = 0.5f;
    [SerializeField] private float gravity = -0.1f;
    [Range(0f, 1f)]
    [SerializeField] private float bounciness = 0.25f;
    [SerializeField] private float maxFallSpeed = 1f;
    [SerializeField] private float maxRaiseSpeed = 1f;

    [Header("Non Adjustable Number")]
    [SerializeField] private float velocity = 0f;
    [SerializeField] private float displacement = 0f;

    // Update is called once per frame
    void Update()
    {
        //Calcs new velocity of slider
        if(Input.GetKeyDown(KeyCode.Space))
        {
            velocity += jumpStrength;
        }
        velocity += gravity * Time.deltaTime;
        velocity = Mathf.Clamp(velocity, -maxFallSpeed, maxRaiseSpeed);

        //Calc new displacement of slider
        displacement += velocity;
        
        //Makes it so the green highlight thingy "bounces" off the top and bottom
        if (displacement < 0f || displacement > 100f)
        {
            velocity = -velocity * bounciness;
        }

        //Restricts green highlight thingy from exceeding its bounds
        displacement = Mathf.Clamp(displacement, 0f, 100f);   

        //Sets position for green highlight thingy
        slider.value = displacement;
    }
}
