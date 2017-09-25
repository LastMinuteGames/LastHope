using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : MonoBehaviour
{
    public Image[] extraEnergys;
    public Image[] energyFills;

	private int baseEnergyCapacity = 5;
    private int energyCapacity = 5;
    private int currentEnergy = 3;

    public void UpdateEnergy(int _currentEnergy)
    {
        currentEnergy = _currentEnergy;

        for (int i = 0; i < currentEnergy; i++)
        {
			energyFills[i].gameObject.SetActive(true);
        }
		for (int i = currentEnergy; i < energyFills.Length; i++)
        {
			energyFills[i].gameObject.SetActive(false);
        }
    }

    public void UpdateEnergyCapacity(int _energyCapacity)
    {
        energyCapacity = _energyCapacity;
		if (energyCapacity < baseEnergyCapacity) {
			return;
		}

		for (int i = 0; i < energyCapacity - baseEnergyCapacity; i++)
        {
			extraEnergys[i].gameObject.SetActive(true);
        }
		for (int i = energyCapacity- baseEnergyCapacity; i < extraEnergys.Length; i++)
        {
			extraEnergys[i].gameObject.SetActive(false);
        }
    }
}