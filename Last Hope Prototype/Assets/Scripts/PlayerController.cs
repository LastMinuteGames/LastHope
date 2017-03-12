using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStance
{
    NEUTRAL,
    RED
}

public class PlayerController : MonoBehaviour
{
    public PlayerStance stance;
    public bool debugMode = false;

    private bool redAbilityEnabled = false;

    // Use this for initialization
    void Start()
    {
        stance = PlayerStance.NEUTRAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            debugMode = !debugMode;
        }
        if (!debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchToNeutral();
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToRed();
            }
        }
    }

    public void SwitchToNeutral()
    {
        Debug.Log("NEUTRAL STANCE");
        stance = PlayerStance.NEUTRAL;
    }

    public void SwitchToRed()
    {
        if (redAbilityEnabled)
        {
            Debug.Log("RED STANCE");
            stance = PlayerStance.RED;
        }
    }

    public void EnableRedAbility()
    {
        redAbilityEnabled = true;
    }
}
