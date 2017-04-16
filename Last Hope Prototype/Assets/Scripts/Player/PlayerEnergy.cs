using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

    public Image currentEnergyBar;

    public int maxEnergy = 5;
    public int currentEnergy;

    private  int initialMaxEnergy;
    private PlayerController playerControl;

    void Awake()
    {
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;
        playerControl = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControl.debugMode)
        {
            if (Input.GetKeyDown(KeyCode.C))
                LoseEnergy(1);

            if (Input.GetKeyDown(KeyCode.V))
                GainEnergy(1);
        }
        UpdateEnergyBar();
    }

    public bool LoseEnergy(int value)
    {
        bool ret = false;
        if (currentEnergy > 0)
        {
            currentEnergy -= value;
            ret = true;
            if (currentEnergy < 0)
            {
                currentEnergy = 0;
            }
        }
        return ret;
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
        if(currentEnergyBar != null)
            currentEnergyBar.rectTransform.localScale = new Vector3(ratio * maxEnergy / initialMaxEnergy, 1, 1);
    }
}