using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Image currentHPBar;

    public int maxHP = 100;
    public int currentHP;
    private int initialMaxHP;
    public Material defaultMaterial;
    public Material dmgedMaterial;
    public float timeBetweenDmg = 0.5f;
    public SpawnManager respawnManager;

    private PlayerMovement moveScript;
    private PlayerAttack attackScript;
    private bool dmged;
    private bool dead;
    private MeshRenderer meshRenderer;
    private float timer;
    private PlayerController playerControl;

    void Awake()
    {
        initialMaxHP = maxHP;
        currentHP = maxHP;
        moveScript = GetComponent<PlayerMovement>();
        attackScript = GetComponent<PlayerAttack>();
        meshRenderer = GetComponent<MeshRenderer>();
        playerControl = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (dmged)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenDmg)
            {
                dmged = false;
                meshRenderer.material = defaultMaterial;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
            TakeDmg(10);

        if (Input.GetKeyDown(KeyCode.H))
            Heal(5);

        UpdateHPBar();
    }

    public void TakeDmg(int value)
    {
        if ((!dead) && (!playerControl.debugMode))
        {
            dmged = true;
            timer = 0;
            currentHP -= value;
            meshRenderer.material = dmgedMaterial;
            if (currentHP <= 0 && !dead)
                Die();
        }
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

    void Die()
    {
        dead = true;
        moveScript.enabled = false;
        attackScript.enabled = false;
        meshRenderer.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    void Respawn()
    {
        if (respawnManager != null)
        {
            transform.position = respawnManager.GetRespawnPoint();
        }
        currentHP = maxHP;
        dead = false;
        moveScript.enabled = true;
        attackScript.enabled = true;
        meshRenderer.enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    void UpdateHPBar()
    {
        float ratio = (float)currentHP / maxHP;
        if(currentHPBar != null)
            currentHPBar.rectTransform.localScale = new Vector3(ratio * maxHP / initialMaxHP, 1, 1);
    }
}