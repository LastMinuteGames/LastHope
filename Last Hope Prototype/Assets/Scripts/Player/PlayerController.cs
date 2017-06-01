using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStance
{
    STANCE_NONE,
    STANCE_BLUE,
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

public struct CameraShakeStats
{
    public float duration;
    public float magnitude;
    public float xMultiplier;
    public float yMultiplier;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Collider katana;
    public MeleeWeaponTrail katanaEmitter;
    public PlayerStance stance = PlayerStance.STANCE_NONE;
    [HideInInspector]
    public PlayerStance newStance = PlayerStance.STANCE_UNDEFINED;
    public bool debugMode = false;

    private bool redAbilityEnabled = false;
    private bool greyAbilityEnabled = false;

    public Image currentHPBar;
    public Image currentEnergyBar;

    public PlayerPassiveStats noneStats;
    public PlayerPassiveStats blueStats;
    public PlayerPassiveStats redStats;

    public CameraShakeStats defaultCameraShakeStats;

    private Attack currentAttack;
    private Attack lastAttackReceived;

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
    public float turnSpeed = 50;
    public float speed = 10;
    public float normalSpeed = 10;
    public float blockingSpeed = 6;
    public Transform camT;
    private CameraShake camShake;
    public Vector3 movement;
    public Vector3 targetDirection;
    public float dodgeThrust = 1;
    public bool pendingMove = false;
    private float movementHorizontal, movementVertical;
    private Rigidbody rigidBody;
    private Vector3 camForward;
    private Vector3 camRight;
    private Dictionary<string, Attack> playerAttacks;

    //UI
    [SerializeField]
    private UIManager uiManager;

    // Interact
    public bool canInteract = false;

    // Block
    public bool blocking = false;

    // Attack
    public float attackDamage = 10.0f;
    private bool inputWindow = false;

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
    //public IPlayerFSM currentState;
    //private Dictionary<PlayerStateType, IPlayerFSM> states;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Stats setup
        noneStats.attackDamage = 10.0f;
        noneStats.blockingMovementSpeed = 6.0f;
        noneStats.movementSpeed = 12.0f;
        noneStats.specialAttackDamage = 0.0f;

        blueStats.attackDamage = 10.0f;
        blueStats.blockingMovementSpeed = 8.0f;
        blueStats.movementSpeed = 16.0f;
        blueStats.specialAttackDamage = 40.0f;

        redStats.attackDamage = 15.0f;
        redStats.blockingMovementSpeed = 6.0f;
        redStats.movementSpeed = 10.0f;
        redStats.specialAttackDamage = 30.0f;

        playerAttacks = new Dictionary<string, Attack>();

        //Light attacks
        playerAttacks.Add("L1", new Attack("L1", 10));
        playerAttacks.Add("L2", new Attack("L2", 15));
        playerAttacks.Add("L3", new Attack("L3", 20));
        
        //Heavy attacks
        playerAttacks.Add("H1", new Attack("H1", 25));
        playerAttacks.Add("H2", new Attack("H2", 30));
        playerAttacks.Add("H3", new Attack("H3", 35));

        //Special Attacks
        playerAttacks.Add("Red", new Attack("Red", 30));
        playerAttacks.Add("Blue", new Attack("Blue", 40));

        //Camera Shake Default Stats
        defaultCameraShakeStats.duration = 0.2f;
        defaultCameraShakeStats.magnitude = 0.5f;
        defaultCameraShakeStats.xMultiplier = 1.0f;
        defaultCameraShakeStats.yMultiplier = 1.0f;


        stance = PlayerStance.STANCE_NONE;

        initialMaxHP = maxHP;
        currentHP = maxHP;
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;

        camT = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camShake = camT.GetComponent<CameraShake>();
        rigidBody = GetComponent<Rigidbody>();

        DisableSwordEmitter();

        uiManager.UpdateMaxHealth(initialMaxHP);
        uiManager.UpdateHealth(currentHP);
        uiManager.UpdateEnergyCapacity(initialMaxEnergy);
        uiManager.UpdateEnergy(currentEnergy);
        uiManager.UpdatePlayerStance(stance);
    }

    public void CallFX()
    {
        GameObject temp1 = GameObject.Find("FxCaller");
        if (temp1 != null)
        {
            FxCaller_OnKey_Pools temp = temp1.GetComponent<FxCaller_OnKey_Pools>();
            if (temp != null)
            {
                temp.ThrowFX();
            }
        }
    }

    public void HeavyAttackEffect()
    {
        //llamada a camera shake de la camara con parámetros razonables, no muy exagerado
        Debug.Log("TERREMOTO!!");
        if (camShake != null)
        {
            StartCoroutine(camShake.Shake(0.1f, 0.25f));
        }
        //TODO: Add attack sound fx when we have one
    }

    void Update()
    {

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

        //UpdateHPBar();
        //UpdateEnergyBar();
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
            case PlayerStance.STANCE_BLUE:
                if (greyAbilityEnabled)
                {
                    Debug.Log("NEUTRAL STANCE");
                    this.stance = stance;
                    normalSpeed = blueStats.movementSpeed;
                    blockingSpeed = blueStats.blockingMovementSpeed;
                    attackDamage = blueStats.attackDamage;
                    specialAttackDamage = blueStats.specialAttackDamage;
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
        uiManager.UpdatePlayerStance(stance);
    }

    public void EnableGreyAbility()
    {
        greyAbilityEnabled = true;
    }

    public void EnableRedAbility()
    {
        redAbilityEnabled = true;
    }

    public bool IsBlueAbilityEnabled()
    {
        return greyAbilityEnabled;
    }

    public bool IsRedAbilityEnabled()
    {
        return redAbilityEnabled;
    }

    public bool TakeDamage(int value)
    {
        if ((!IsDead()) && (!debugMode))
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
            uiManager.UpdateHealth(currentHP);
        }
        return false;
    }

    public void Heal(int value)
    {
        if (!IsDead() && currentHP < maxHP)
        {
            currentHP += value;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            uiManager.UpdateHealth(currentHP);
        }
    }

    public void IncreaseMaxHealthAndHeal(int value)
    {
        maxHP += value;
        currentHP = maxHP;
        uiManager.UpdateMaxHealth(maxHP);
        uiManager.UpdateHealth(currentHP);
    }

    public void Die()
    {
        dead = true;
    }

    public bool IsDead()
    {
        return dead;
    }

    public void Respawn()
    {
        anim.SetTrigger("respawn");
        if (respawnManager != null)
        {
            transform.position = respawnManager.GetRespawnPoint();
        }
        currentHP = maxHP;
        uiManager.UpdateHealth(currentHP);
        dead = false;
    }

    public bool CanLoseEnergy(int value)
    {
        bool ret = false;
        if (currentEnergy >= value)
        {
            ret = true;
        }
        return ret;
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
            uiManager.UpdateEnergy(currentEnergy);
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
            uiManager.UpdateEnergy(currentEnergy);
        }
    }

    public void ChangeAttack(string name)
    {
        currentAttack = playerAttacks[name];
    }

    public void IncreaseMaxEnergy(int value)
    {
        maxEnergy += value;
        currentEnergy = maxEnergy;
        uiManager.UpdateEnergyCapacity(maxEnergy);
        uiManager.UpdateEnergy(currentEnergy);
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

    public PlayerStance SpecialAttackToPerform()
    {
        PlayerStance ret = PlayerStance.STANCE_UNDEFINED;
        canSpecialAttack = false;
        if (stance != PlayerStance.STANCE_NONE)
        {
            if (CanLoseEnergy(1))
            {
                canSpecialAttack = true;
                ret = stance;
            }
            else
            {
                ret = PlayerStance.STANCE_NONE;
            }
        }
        else
        {
            ret = PlayerStance.STANCE_NONE;
        }
        return ret;
    }

    public void StartBlueSpecialAttack()
    {
        canSpecialAttack = false;
        if (stance == PlayerStance.STANCE_BLUE)
        {
            if (LoseEnergy(1))
            {
                canSpecialAttack = true;
                neutralSphere.gameObject.SetActive(true);
                spawnedParticle = Instantiate(neutralAttackParticles, neutralSphere.transform.position, neutralSphere.transform.rotation);
                if (camShake != null)
                {
                    StartCoroutine(camShake.Shake());
                }
            }
        }
    }
    public void StartRedSpecialAttack()
    {
        canSpecialAttack = false;
        if (stance == PlayerStance.STANCE_RED)
        {
            if (LoseEnergy(1))
            {
                canSpecialAttack = true;
                redSpehre.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateRedSpecialAttack()
    {
        if (canSpecialAttack)
        {
            movement = rigidBody.velocity;
            Vector3 impulse = targetDirection.normalized * redSpecialAttackThrust;
            movement += impulse;
            rigidBody.velocity = movement;
        }
    }

    public void EndBlueSpecialAttack()
    {
        if (canSpecialAttack)
        {
            neutralSphere.gameObject.SetActive(false);
            Destroy(spawnedParticle);
        }
    }

    public void EndRedSpecialAttack()
    {
        if (canSpecialAttack)
        {
            redSpehre.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null && interactable.CanInteract())
            {
                canInteract = true;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            EnemyTrash trashScript = other.gameObject.GetComponentInParent<EnemyTrash>();
            Attack currentAttackReceived = trashScript.GetAttack();
            //int damage = 10;
            if (currentAttackReceived != null)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Block") == false)
                {
                    TakeDamage(currentAttackReceived.damage);
                    anim.SetTrigger("damaged");
                }
            }
            lastAttackReceived = currentAttackReceived;
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

    private void UpdateEnergyBar()
    {
        float ratio = (float)currentEnergy / maxEnergy;
        currentEnergyBar.rectTransform.localScale = new Vector3(ratio * maxEnergy / initialMaxEnergy, 1, 1);
    }

    //public void ChangeState(PlayerStateType type)
    //{
    //if (states.ContainsKey(type))
    //{
    //    if (currentState != null)
    //        currentState.End();
    //    currentState = states[type];
    //    currentState.Start();
    //    currentStateType = currentState.Type();
    //}
    //}

    public void OpenInputWindow()
    {
        inputWindow = true;
        //Debug.Log("window opened");
    }

    public void CloseInputWindow()
    {
        inputWindow = false;
        //Debug.Log("window closed");
    }

    public bool GetInputWindowState()
    {
        return inputWindow;
    }

    public Attack GetAttack()
    {
        return currentAttack == null ? null : playerAttacks[currentAttack.name];
    }

    public void EnableSwordEmitter()
    {
        katanaEmitter.Emit = true;
    }

    public void DisableSwordEmitter()
    {
        katanaEmitter.Emit = false;
    }
}
