using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

public class Health : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] private Image healthSlider;
    [SerializeField] private GameObject blood;
    [SerializeField] private float health = 100;
    private float maxHealth = 100;
    private bool bleeding = false;

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

    private void Update()
    {
        if(bleeding)
        {
            health -= 5 * Time.deltaTime;
            SetHealthBar();
        }
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

    public void StartBleeding()
    {
        bleeding = true;
        FishMovement[] fishies = FindObjectsOfType<FishMovement>();
        foreach (var fish in fishies)
        {
            //this only runs if the fish is actually an eyeless fish
            fish.ChangeEyelessReaction();
        }
        Instantiate(blood, FindAnyObjectByType<FirstPersonController>().transform);
        StartCoroutine(Bleed(Random.Range(2f, 5f)));
    }

    private IEnumerator Bleed(float time)
    {
        yield return new WaitForSeconds(time);
        bleeding = false;
    }
}
