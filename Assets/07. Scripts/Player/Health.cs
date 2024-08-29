using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private Slider healthSlider;

    [SerializeField] private float health = 100;

    private void Start()
    {
        healthSlider.value = health;
    }

    public void ChangeHealth(int change)
    {
        health += change;
        healthSlider.value = health;
    }
}
