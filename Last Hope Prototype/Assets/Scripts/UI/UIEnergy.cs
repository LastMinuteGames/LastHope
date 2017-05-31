using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : MonoBehaviour
{
    public int maxEnergy = 7;
    public int startingEnergy = 1;
    public Image[] energyBars;
    public Image[] fills;
    public bool testModeOn = false;

    private int energyCapacity;
    private int currentEnergy;


    //void Start()
    //{
    //    if (startingEnergy > maxEnergy)
    //    {
    //        startingEnergy = maxEnergy;
    //    }

    //    energyCapacity = startingEnergy;
    //    currentEnergy = startingEnergy;
    //    RenderUI();
    //}

    //public void Update()
    //{
    //    //TODO::only enable this on debug mode and probably change keybindings
    //    if (!testModeOn)
    //    {
    //        return;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        OnEnergyCapacityIncrease();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        OnEnergyCapacityDecrease();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        OnCurrentEnergyIncrease();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        OnCurrentEnergyDecrease();
    //    }
    //}

    //public void OnEnergyCapacityIncrease()
    //{
    //    if (energyCapacity < maxEnergy)
    //    {
    //        energyCapacity++;
    //        currentEnergy++;
    //        RenderUI();
    //    }
    //}

    //public void OnEnergyCapacityDecrease()
    //{
    //    if (energyCapacity > 1)
    //    {
    //        energyCapacity--;
    //        if (currentEnergy > 0)
    //        {
    //            currentEnergy--;
    //        }
    //        RenderUI();
    //    }
    //}

    //public void OnCurrentEnergyIncrease()
    //{
    //    if (currentEnergy < energyCapacity)
    //    {
    //        currentEnergy++;
    //        RenderUI();
    //    }
    //}

    //public void OnCurrentEnergyDecrease()
    //{
    //    if (currentEnergy > 0)
    //    {
    //        currentEnergy--;
    //        RenderUI();
    //    }
    //}

    //void RenderUI()
    //{
    //    for (int i = 0; i < energyCapacity; i++)
    //    {
    //        energyBars[i].gameObject.SetActive(true);
    //    }
    //    for (int i = energyCapacity; i < energyBars.Length; i++)
    //    {
    //        energyBars[i].gameObject.SetActive(false);
    //    }

    //    for (int i = 0; i < currentEnergy; i++)
    //    {
    //        fills[i].gameObject.SetActive(true);
    //    }
    //    for (int i = currentEnergy; i < fills.Length; i++)
    //    {
    //        fills[i].gameObject.SetActive(false);
    //    }
    //}



    public void UpdateEnergy(int _currentEnergy)
    {
        currentEnergy = _currentEnergy;

        for (int i = 0; i < currentEnergy; i++)
        {
            fills[i].gameObject.SetActive(true);
        }
        for (int i = currentEnergy; i < fills.Length; i++)
        {
            fills[i].gameObject.SetActive(false);
        }

    }

    public void UpdateEnergyCapacity(int _energyCapacity)
    {
        energyCapacity = _energyCapacity;
        for (int i = 0; i < energyCapacity; i++)
        {
            energyBars[i].gameObject.SetActive(true);
        }
        for (int i = energyCapacity; i < energyBars.Length; i++)
        {
            energyBars[i].gameObject.SetActive(false);
        }


    }

}
