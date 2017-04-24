using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStance
{
    STANCE_NONE,
    STANCE_GREY,
    STANCE_RED,
    STANCE_UNDEFINED
}

public struct PlayerPassiveStats
{
    public float attackDamage;
    //public float attackSpeed;
    public float blockingMovementSpeed;
    public float movementSpeed;
    public float specialAttackDamage;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Collider katana;
    public PlayerStance stance = PlayerStance.STANCE_NONE;
    [HideInInspector]
    public PlayerStance newStance = PlayerStance.STANCE_UNDEFINED;
    public bool debugMode = false;

    private bool redAbilityEnabled = false;
    private bool greyAbilityEnabled = false;

    public Image currentHPBar;
    public Image currentEnergyBar;

    public PlayerPassiveStats noneStats;
    public PlayerPassiveStats neutralStats;
    public PlayerPassiveStats redStats;

    // HP
    public int maxHP = 100;
    public int currentHP;
    private int initialMaxHP;
    public float timeBetweenDmg = 0.5f;
    public SpawnManager respawnManager;
    private PlayerAttack attackScript;
    private bool dmged;
    private bool dead;
    private float timer;

    // Energy
    public int maxEnergy = 5;
    public int currentEnergy;
    private int initialMaxEnergy;

    // Movement
    public float turnSpeed = 3;
    public float speed = 10;
    public float normalSpeed = 10;
    public float blockingSpeed = 6;
    public Transform camT;
    public Vector3 movement;
    public Vector3 targetDirection;
    public float dodgeThrust = 25;
    public bool pendingMove = false;
    private float movementHorizontal, movementVertical;
    private Rigidbody rigidBody;
    private Vector3 camForward;
    private Vector3 camRight;

    // Interact
    public bool canInteract = false;

    // Block
    public bool blocking = false;

    // Attack
    public float attackDamage = 10.0f;

    // Special Attack
    public GameObject neutralSphere;
    public GameObject neutralAttackParticles;
    private GameObject spawnedParticle;
    public GameObject redSpehre;
    public float redSpecialAttackThrust = 30;
    public float specialAttackDamage = 40;
    private bool canSpecialAttack = false;

    [SerializeField]
    private PlayerStateType currentStateType;
    private IPlayerFSM currentState;
    private Dictionary<PlayerStateType, IPlayerFSM> states;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Stats setup
        noneStats.attackDamage = 10.0f;
        noneStats.blockingMovementSpeed = 6.0f;
        noneStats.movementSpeed = 12.0f;
        noneStats.specialAttackDamage = 0.0f;

        neutralStats.attackDamage = 10.0f;
        neutralStats.blockingMovementSpeed = 8.0f;
        neutralStats.movementSpeed = 14.0f;
        neutralStats.specialAttackDamage = 40.0f;

        redStats.attackDamage = 15.0f;
        redStats.blockingMovementSpeed = 6.0f;
        redStats.movementSpeed = 12.0f;
        redStats.specialAttackDamage = 30.0f;


        stance = PlayerStance.STANCE_NONE;

        initialMaxHP = maxHP;
        currentHP = maxHP;
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;

        camT = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rigidBody = GetComponent<Rigidbody>();

        IPlayerFSM state = new PlayerIdleState(gameObject);
        PlayerStateType defaultState = state.Type();
        states = new Dictionary<PlayerStateType, IPlayerFSM>();
        states.Add(state.Type(), state);

        state = new PlayerBlockState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerMoveBlockState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerDamageState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerDeadState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerRespawnState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerDodgeState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerInteractState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerAttackState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerSpecialAttackState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerChangeStanceState(gameObject);
        states.Add(state.Type(), state);

        state = new PlayerMoveState(gameObject);
        states.Add(state.Type(), state);

        ChangeState(defaultState);
    }

    void Update()
    {
        PlayerStateType state = currentState.Update();
        if (state != PlayerStateType.PLAYER_STATE_UNDEFINED && state != currentStateType)
        {
            ChangeState(state);
        }

        if (InputManager.DebugMode())
        {
            debugMode = !debugMode;
        }
        if (debugMode)
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

    void FixedUpdate()
    {
        if (pendingMove)
        {
            Rotate();
            Move();
        }
    }

    public void ChangeStance(PlayerStance stance)
    {
        switch (stance)
        {
            case PlayerStance.STANCE_GREY:
                if (greyAbilityEnabled)
                {
                    Debug.Log("NEUTRAL STANCE");
                    this.stance = stance;
                    normalSpeed = neutralStats.movementSpeed;
                    blockingSpeed = neutralStats.blockingMovementSpeed;
                    attackDamage = neutralStats.attackDamage;
                    specialAttackDamage = neutralStats.specialAttackDamage;
                }
                break;
            case PlayerStance.STANCE_RED:
                if (redAbilityEnabled)
                {
                    Debug.Log("RED STANCE");
                    this.stance = stance;
                    normalSpeed = redStats.movementSpeed;
                    blockingSpeed = redStats.blockingMovementSpeed;
                    attackDamage = redStats.attackDamage;
                    specialAttackDamage = redStats.specialAttackDamage;
                }
                break;
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

    public bool IsGreyAbilityEnabled()
    {
        return greyAbilityEnabled;
    }

    public bool IsRedAbilityEnabled()
    {
        return redAbilityEnabled;
    }

    public bool TakeDmg(int value)
    {
        if ((!dead) && (!debugMode))
        {
            return LoseHp(value);
        }
        return false;
    }

    private bool LoseHp(int value)
    {
        if (!blocking)
        {
            dmged = true;
            timer = 0;
            currentHP -= value;
            if (currentHP <= 0 && !dead)
            {
                Die();
                return true;
            }
        }
        return false;
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
    }

    public void Respawn()
    {
        if (respawnManager != null)
        {
            transform.position = respawnManager.GetRespawnPoint();
        }
        currentHP = maxHP;
        dead = false;
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

    public void Rotate()
    {
        camForward = camT.TransformDirection(Vector3.forward);
        camForward.y = 0;
        camForward.Normalize();

        Debug.DrawRay(transform.position, camForward, Color.black);
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);

        camRight = new Vector3(camForward.z, 0, -camForward.x);
        Debug.DrawRay(transform.position, camRight, Color.red);

        targetDirection = camForward * movementVertical + camRight * movementHorizontal;
        Debug.DrawRay(transform.position, targetDirection, Color.blue);

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public void PendingMovement(float h, float v)
    {
        movementHorizontal = h;
        movementVertical = v;
        pendingMove = true;
    }

    public void Move()
    {
        movement = rigidBody.velocity;

        movement.z = 0;
        movementHorizontal = Mathf.Abs(movementHorizontal);
        movementVertical = Mathf.Abs(movementVertical);
        float totalImpulse = movementHorizontal + movementVertical;
        totalImpulse = (totalImpulse > 1) ? 1 : totalImpulse;
        movement.z += totalImpulse * speed;

        movement.x = 0;

        if (movement.y > 2)
            movement.y = 2;

        rigidBody.velocity = transform.TransformDirection(movement);

        pendingMove = false;
    }

    public void StartAttack()
    {
        katana.enabled = true;
    }

    public void EndAttack()
    {
        katana.enabled = false;
    }

    public void Dodge()
    {
        movement = rigidBody.velocity;
        Vector3 impulse = targetDirection.normalized * dodgeThrust;
        movement += impulse;
        rigidBody.velocity = movement;
    }

    public void SetBlocking(bool value)
    {
        blocking = value;
    }

    public void StartSpecialAttack()
    {
        canSpecialAttack = false;
        if (stance != PlayerStance.STANCE_NONE)
        {
            if (LoseEnergy(1))
            {
                canSpecialAttack = true;
                switch (stance)
                {
                    case PlayerStance.STANCE_GREY:
                        neutralSphere.gameObject.SetActive(true);
                        spawnedParticle = Instantiate(neutralAttackParticles, neutralSphere.transform.position, neutralSphere.transform.rotation);
                        break;
                    case PlayerStance.STANCE_RED:
                        redSpehre.gameObject.SetActive(true);
                        break;
                }
            }
        }
    }
    public void SpecialAttack()
    {
        if (canSpecialAttack)
        {
            switch (stance)
            {
                case PlayerStance.STANCE_GREY:
                    break;
                case PlayerStance.STANCE_RED:
                    movement = rigidBody.velocity;
                    Vector3 impulse = targetDirection.normalized * redSpecialAttackThrust;
                    movement += impulse;
                    rigidBody.velocity = movement;
                    break;
            }
        }
    }

    public void EndSpecialAttack()
    {
        if (canSpecialAttack)
        {
            switch (stance)
            {
                case PlayerStance.STANCE_GREY:
                    neutralSphere.gameObject.SetActive(false);
                    Destroy(spawnedParticle);
                    break;
                case PlayerStance.STANCE_RED:
                    redSpehre.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            currentState.OnTriggerEnter(other);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            canInteract = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        // TODO: Possible bug if two interactable triggers are colliding with the player and one exits
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            canInteract = false;
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

    public void ChangeState(PlayerStateType type)
    {
        if (states.ContainsKey(type))
        {
            if (currentState != null)
                currentState.End();
            currentState = states[type];
            currentState.Start();
            currentStateType = currentState.Type();
        }
    }
}
