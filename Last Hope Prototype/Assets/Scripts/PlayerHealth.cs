using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int starthp = 100;
    public int currenthp;
    public Material defaultMaterial;
    public Material dmgedMaterial;
    public float timeBetweenDmg = 0.5f;

    private PlayerMovement movescript;
    private bool dmged;
    private bool dead;
    private MeshRenderer meshRenderer;
    private float timer;

    void Awake()
    {
        currenthp = starthp;
        movescript = GetComponent<PlayerMovement>();
        meshRenderer = GetComponent<MeshRenderer>();
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

        if (dead == true && Input.GetKeyDown(KeyCode.R))
            Respawn();

    }

    public void TakeDmg(int nasusQ)
    {
        if (!dead)
        {
            dmged = true;
            timer = 0;
            currenthp -= nasusQ;
            meshRenderer.material = dmgedMaterial;
            if (currenthp <= 0 && !dead)
                Die();
        }
    }

    void Die()
    {
        dead = true;
        movescript.enabled = false;
        meshRenderer.enabled = false;
    }

    void Respawn()
    {
        currenthp = starthp;
        dead = false;
        movescript.enabled = true;
        meshRenderer.enabled = true;
    }


}
