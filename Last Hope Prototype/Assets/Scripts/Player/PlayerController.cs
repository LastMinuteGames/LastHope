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
        if (InputManager.DebugMode())
        {
            debugMode = !debugMode;
        }
        if (!debugMode)
        {
            if (InputManager.Stance1())
            {
                SwitchToNeutral();
            } else if (InputManager.Stance2())
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            int damage = other.gameObject.transform.parent.gameObject.GetComponent<EnemyTrash>().attack;
            gameObject.GetComponent<PlayerHealth>().TakeDmg(damage);
        }
    }
}
