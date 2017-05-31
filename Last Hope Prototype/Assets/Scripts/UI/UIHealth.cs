using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int maxHealth = 100;
    public bool testModeOn = false;

    public int currentHealth;
    private Slider hpSlider;

    void Awake()
    {
        hpSlider = GetComponent<Slider>();
    }

    //void Start()
    //{
    //    if (startingHealth > maxHealth)
    //    {
    //        startingHealth = maxHealth;
    //    }
    //    currentHealth = startingHealth;
    //    hpSlider = GetComponent<Slider>();
    //    RenderUI();
    //}

    //void Update()
    //{
    //    //TODO::only enable this on debug mode and probably change keybindings
    //    if (!testModeOn)
    //    {
    //        return;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        OnIncrementMaxHealth(10);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        OnDecreaseMaxHealth(10);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        HealUp(10);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        TakeDamage(10);
    //    }
    //}

    //public void OnIncrementMaxHealth(int amount)
    //{
    //    RenderUI();
    //}

    //public void OnDecreaseMaxHealth(int amount)
    //{
    //    RenderUI();
    //}

    //public void HealUp(int amount)
    //{
    //    currentHealth += amount;
    //    if (currentHealth > maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //    }
    //    RenderUI();
    //}

    //public void TakeDamage(int amount)
    //{
    //    if (currentHealth > 0)
    //    {
    //        currentHealth -= amount;
    //        if (currentHealth <= 0)
    //        {
    //            currentHealth = 0;
    //            Die();
    //        }
    //    }
    //    RenderUI();
    //}

    //void Die()
    //{
    //    //die behaviour
    //}

    //void RenderUI()
    //{
    //    hpSlider.value = (float)currentHealth / 100;
    //}


    public void UpdateHealth(int amount)
    {
        currentHealth = amount;
        hpSlider.value = (float)currentHealth / maxHealth;
    }

    public void UpdateMaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        // chage hp bar size

    }
}
