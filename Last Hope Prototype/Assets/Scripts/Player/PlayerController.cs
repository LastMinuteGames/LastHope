using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image currentHPBar;
    public Image currentEnergyBar;

    // HP
    public int maxHP = 100;
    public int currentHP;
    private int initialMaxHP;
    public float timeBetweenDmg = 0.5f;
    public SpawnManager respawnManager;
    private PlayerMovement moveScript;
    private PlayerAttack attackScript;
    private bool dmged;
    private bool dead;
    private float timer;

    // Energy
    public int maxEnergy = 5;
    public int currentEnergy;
    private int initialMaxEnergy;

    IPlayerFSM currentState;
    PlayerStateType currentStateType;
    Dictionary<PlayerStateType, IPlayerFSM> states;

    // Use this for initialization
    void Start()
    {
        stance = PlayerStance.STANCE_NONE;

        initialMaxHP = maxHP;
        currentHP = maxHP;
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;

        moveScript = GetComponent<PlayerMovement>();
        attackScript = GetComponent<PlayerAttack>();

        IPlayerFSM state = new PlayerMoveState(gameObject);
        PlayerStateType defaultState = state.Type();
        states = new Dictionary<PlayerStateType, IPlayerFSM>();
        states.Add(state.Type(), state);

        ChangeState(defaultState);
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerStateType state = currentState.Update();
        if(state!= PlayerStateType.PLAYER_STATE_UNDEFINED && state != currentStateType)
        {
            ChangeState(state);
        }


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
        } else
        {
            if (Input.GetKeyDown(KeyCode.T))
                LoseHp(10);

            if (Input.GetKeyDown(KeyCode.H))
                Heal(5);

            if (Input.GetKeyDown(KeyCode.C))
                LoseEnergy(1);

            if (Input.GetKeyDown(KeyCode.V))
                GainEnergy(1);
        }
        if (dmged)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenDmg)
            {
                dmged = false;
            }
        }
        
        UpdateHPBar();
        UpdateEnergyBar();
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

    public void TakeDmg(int value)
    {
        if ((!dead) && (!debugMode))
        {
            LoseHp(value);
        }
    }

    private void LoseHp(int value)
    {
        dmged = true;
        timer = 0;
        currentHP -= value;
        if (currentHP <= 0 && !dead)
            Die();
    }

    public void Heal(int value)
    {
        if (!dead && currentHP < maxHP)
        {
            currentHP += value;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    public void IncreaseMaxHealthAndHeal(int value)
    {
        maxHP += value;
        currentHP = maxHP;
    }

    private void Die()
    {
        dead = true;
        moveScript.enabled = false;
        attackScript.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    private void Respawn()
    {
        if (respawnManager != null)
        {
            transform.position = respawnManager.GetRespawnPoint();
        }
        currentHP = maxHP;
        dead = false;
        moveScript.enabled = true;
        attackScript.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            int damage = other.gameObject.transform.parent.gameObject.GetComponent<EnemyTrash>().attack;
            TakeDmg(damage);
        }
    }

    private void UpdateHPBar()
    {
        float ratio = (float)currentHP / maxHP;
        currentHPBar.rectTransform.localScale = new Vector3(ratio * maxHP / initialMaxHP, 1, 1);
    }

    void UpdateEnergyBar()
    {
        float ratio = (float)currentEnergy / maxEnergy;
        currentEnergyBar.rectTransform.localScale = new Vector3(ratio * maxEnergy / initialMaxEnergy, 1, 1);
    }

    void ChangeState(PlayerStateType type)
    {
        if (states.ContainsKey(type))
        {
            if(currentState != null)
                currentState.End();
            currentState = states[type];
            currentState.Start();
        }
    }
}
