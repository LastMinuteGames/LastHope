using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    UIHealth uiHealth;
    [SerializeField]
    UIEnergy uiEnergy;
    [SerializeField]
    UIPlayerStance uiPlayerStance;


    //HP
    public void UpdateHealth(int currentHp)
    {
        uiHealth.UpdateHealth(currentHp);
    }
    public void UpdateMaxHealth(int maxHealth)
    {
        uiHealth.UpdateMaxHealth(maxHealth);
    }

    //Energy
    public void UpdateEnergy(int currentEnergy)
    {
        uiEnergy.UpdateEnergy(currentEnergy);
    }
    public void UpdateEnergyCapacity(int energyCapacity)
    {
        uiEnergy.UpdateEnergyCapacity(energyCapacity);
    }


    //Stance
    public void UpdatePlayerStance(PlayerStance playerStance)
    {
        uiPlayerStance.UpdatePlayerStance(playerStance);
    }



}
