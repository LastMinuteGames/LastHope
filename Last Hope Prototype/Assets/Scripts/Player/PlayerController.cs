using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStance
{
    STANCE_NONE,
    STANCE_GREY,
    STANCE_RED
}

public class PlayerController : MonoBehaviour
{
    public PlayerStance stance;
    public bool debugMode = false;

    private bool redAbilityEnabled = false;
    private bool greyAbilityEnabled = false;

    // Use this for initialization
    void Start()
    {
        stance = PlayerStance.STANCE_NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.DebugMode())
        {
            debugMode = !debugMode;
        }
        if (!debugMode)
        {
            if (InputManager.Stance1())
            {
                SwitchToGrey();
            }
            else if (InputManager.Stance2())
            {
                SwitchToRed();
            }
        }
    }

    public void SwitchToGrey()
    {
        if (greyAbilityEnabled)
        {
            Debug.Log("NEUTRAL STANCE");
            stance = PlayerStance.STANCE_GREY;
        }
    }

    public void SwitchToRed()
    {
        if (redAbilityEnabled)
        {
            Debug.Log("RED STANCE");
            stance = PlayerStance.STANCE_RED;
        }
    }

    public void EnableGreyAbility()
    {
        greyAbilityEnabled = true;
    }

    public void EnableRedAbility()
    {
        redAbilityEnabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            int damage = other.gameObject.transform.parent.gameObject.GetComponent<EnemyTrash>().attack;
            gameObject.GetComponent<PlayerHealth>().TakeDmg(damage);
        }
    }
}
