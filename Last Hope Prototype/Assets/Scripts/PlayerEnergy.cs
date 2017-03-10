using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour {

    public Image currentEnergyBar;

    public int maxEnergy = 100;
    public int currenEnergy;

    void Awake()
    {
        currenEnergy = maxEnergy;
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
        if (currenEnergy > 0)
        {
            currenEnergy -= value;
            if (currenEnergy < 0)
            {
                currenEnergy = 0;
            }
        }
    }
    public void GainEnergy(int value)
    {
        if (currenEnergy < maxEnergy)
        {
            currenEnergy += value;
            if (currenEnergy > maxEnergy)
            {
                currenEnergy = maxEnergy;
            }
        }
    }

    void UpdateEnergyBar()
    {
        float ratio = (float)currenEnergy / maxEnergy;
        currentEnergyBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
}