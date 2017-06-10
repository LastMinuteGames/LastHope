using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Collider sword;
    public MeleeWeaponTrail swordEmitter;
    public Collider shield;
    public MeleeWeaponTrail shieldEmitter;
    public GameObject hitParticles;
    public ParticleSystem redAbilityParticles;
    [HideInInspector]
    public PlayerStance stance;// = PlayerStance.STANCE_NONE;
    [HideInInspector]
    public PlayerStanceType newStance;
    public bool debugMode = false;

    private bool redAbilityEnabled = false;
    private bool greyAbilityEnabled = false;

    public Image currentHPBar;
    public Image currentEnergyBar;

    //public PlayerPassiveStats noneStats;
    public PlayerPassiveStatsAbsolute baseStats;
    public PlayerPassiveStatsRelative blueStats;
    public PlayerPassiveStatsRelative redStats;

    public PlayerPassiveStatsAbsolute currentStats;

    public CameraShakeStats defaultCameraShakeStats;

    private Attack currentAttack;
    //private Attack lastAttackReceived;

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
    //public float speed = 10;
    public Transform camT;
    private CameraShake camShake;
    public Vector3 movement;
    public Vector3 targetDirection;
    public float dodgeThrust;// = 5;
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
    private bool inputWindow = false;

    // Special Attack
    public GameObject neutralSphere;
    public GameObject neutralAttackParticles;
    private GameObject spawnedParticle;
    public GameObject redSpehre;
    public float redSpecialAttackThrust = 30;
    public float specialAttackDamage = 40;
    private bool canSpecialAttack = false;

    private bool isDodge = false;
    public Dictionary<PlayerStanceType, PlayerStance> stances;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Stats setup
        stances = new Dictionary<PlayerStanceType, PlayerStance>();
        stances.Add(PlayerStanceType.STANCE_NONE, new PlayerStance(PlayerStanceType.STANCE_NONE, new PlayerPassiveStatsRelative(1, 1, 1, 1)));
        stances.Add(PlayerStanceType.STANCE_BLUE, new PlayerStance(PlayerStanceType.STANCE_BLUE, new PlayerPassiveStatsRelative(1, 0.66f, 1.33f, 40)));
        stances.Add(PlayerStanceType.STANCE_RED, new PlayerStance(PlayerStanceType.STANCE_RED, new PlayerPassiveStatsRelative(1.5f, 1, 0.85f, 30)));
        currentStats = baseStats;

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

        stance = stances[PlayerStanceType.STANCE_NONE];

        initialMaxHP = maxHP;
        currentHP = maxHP;
        initialMaxEnergy = maxEnergy;
        currentEnergy = maxEnergy;

        camT = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camShake = camT.GetComponent<CameraShake>();
        rigidBody = GetComponent<Rigidbody>();

        DisableSwordEmitter();
        DisableShieldEmitter();

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
        if (camShake != null)
        {
            StartCoroutine(camShake.Shake(0.1f, 0.25f,1,1,this.transform));
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            Dodge();
        } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RedSpecialAttack"))
        {
            UpdateRedSpecialAttack();
        }
        else if (pendingMove)
        {
            Rotate();
            Move();
            pendingMove = false;
        }
    }

    public void ChangeStance(PlayerStanceType typeStance)
    {
        switch (typeStance)
        {
            case PlayerStanceType.STANCE_BLUE:
                if (greyAbilityEnabled)
                {
                    Debug.Log("NEUTRAL STANCE");
                    this.stance = stances[typeStance];
                    currentStats = baseStats * blueStats;
                }
                break;
            case PlayerStanceType.STANCE_RED:
                if (redAbilityEnabled)
                {
                    Debug.Log("RED STANCE");
                    this.stance = stances[typeStance];
                    currentStats = baseStats * redStats;
                }
                break;
        }
        uiManager.UpdatePlayerStance(stance);
    }

    public void EnableBlueAbility()
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

        movement = Vector3.zero;
        //movement.z = 0;
        movementHorizontal = Mathf.Abs(movementHorizontal);
        movementVertical = Mathf.Abs(movementVertical);
        float totalImpulse = movementHorizontal + movementVertical;
        totalImpulse = (totalImpulse > 1) ? 1 : totalImpulse;
        movement.x = totalImpulse * targetDirection.x;
        movement.z = totalImpulse * targetDirection.z;

        rigidBody.MovePosition(rigidBody.position  +  movement * currentStats.movementSpeed * Time.deltaTime);

    }

    public void StartSwordAttack()
    {
        sword.enabled = true;
    }

    public void EndSwordAttack()
    {
        sword.enabled = false;
    }

    public void StartShieldAttack()
    {
        shield.enabled = true;
    }

    public void EndShieldAttack()
    {
        shield.enabled = false;
    }

    public void Dodge()
    {
        rigidBody.MovePosition(transform.position + transform.forward * dodgeThrust * Time.deltaTime);
        //movement = rigidBody.velocity;
        //Vector3 impulse = targetDirection.normalized * dodgeThrust;
        //movement += impulse;
        //rigidBody.velocity = movement;
    }

    public void SetBlocking(bool value)
    {
        blocking = value;
    }

    public PlayerStanceType SpecialAttackToPerform()
    {
        PlayerStanceType ret = PlayerStanceType.STANCE_NONE;
        canSpecialAttack = false;
        if (stance.type != PlayerStanceType.STANCE_NONE && CanLoseEnergy(1))
        {
            canSpecialAttack = true;
            ret = stance.type;
        }

        return ret;
    }

    public void StartBlueSpecialAttack()
    {
        canSpecialAttack = false;
        if (stance.type == PlayerStanceType.STANCE_BLUE && LoseEnergy(1))
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
    public void StartRedSpecialAttack()
    {
        canSpecialAttack = false;
        if (stance.type == PlayerStanceType.STANCE_RED)
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
            rigidBody.MovePosition(transform.position + transform.forward * redSpecialAttackThrust * Time.deltaTime);
            /*movement = rigidBody.velocity;
            Vector3 impulse = targetDirection.normalized * redSpecialAttackThrust;
            movement += impulse;
            rigidBody.velocity = movement;
            */

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
            if (currentAttackReceived != null)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Block") == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Die") == false)
                {
                    TakeDamage(currentAttackReceived.damage);
                    anim.SetTrigger("damaged");
                }
            }
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
        swordEmitter.Emit = true;
    }

    public void DisableSwordEmitter()
    {
        swordEmitter.Emit = false;
    }

    public void EnableShieldEmitter()
    {
        shieldEmitter.Emit = true;
    }

    public void DisableShieldEmitter()
    {
        shieldEmitter.Emit = false;
   }
    public bool IsDodge
    {
        get
        {
            return isDodge;
        }

        set
        {
            isDodge = value;
        }
    }

    public void SpawnHitParticles(Vector3 position)
    {
        GameObject particle = (GameObject)Instantiate(hitParticles, position, transform.rotation);
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(particle, totalDuration);
    }

    public void PlayRedAbilityParticles()
    {
        redAbilityParticles.Play();
    }

    public void StopRedAbilityParticles()
    {
        redAbilityParticles.Stop();
    }
}
