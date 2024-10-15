using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private Image healthSlider;

    [SerializeField] private float health = 100;
    private float maxHealth = 100;

    private void Start()
    {
        SetHealthBar();
    }

    public void ChangeHealth(float change)
    {
        health += change;
        if(health <= 0)
        {
            SceneManager.LoadScene("4. Death");
        }

        SetHealthBar();
    }

    public void SetMaxHealth(float input)
    {
        float healthIncrease = input / maxHealth;
        maxHealth = input;
        health *= healthIncrease;
    }

    private void SetHealthBar()
    {
        healthSlider.fillAmount = health / maxHealth;
    }
}
