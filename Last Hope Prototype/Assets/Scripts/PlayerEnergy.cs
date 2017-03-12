using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

    public Image currentEnergyBar;

    public int maxEnergy = 5;
    public int currentEnergy;

    private  int initialMaxEnergy;

    void Awake()
    {
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            LoseEnergy(1);

        if (Input.GetKeyDown(KeyCode.V))
            GainEnergy(1);

        UpdateEnergyBar();
    }

    public void LoseEnergy(int value)
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= value;
            if (currentEnergy < 0)
            {
                currentEnergy = 0;
            }
        }
    }
    public void GainEnergy(int value)
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += value;
            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }
    }

    public void IncreaseMaxEnergy(int value)
    {
        maxEnergy += value;
        currentEnergy = maxEnergy;
    }

    void UpdateEnergyBar()
    {
        float ratio = (float)currentEnergy / maxEnergy;
        currentEnergyBar.rectTransform.localScale = new Vector3(ratio * maxEnergy / initialMaxEnergy, 1, 1);
    }
}