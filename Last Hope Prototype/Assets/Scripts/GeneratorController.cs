using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : Interactable {

    public GameObject energyCore;
    public Vector3 spawnPointPos;
    public Quaternion spawnPointQuat;

    private Animator animator;

    bool running = false;

    void Start () {
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorNoise);
        animator = GetComponentInChildren<Animator>();
    }
	
	void Update () {

    }

    public override void Run()
    {
        if(CanInteract())
        {
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_PlayerToWorld_Interact);
            animator.SetTrigger("Charging");
            running = true;
            Invoke("SpawnSpecialAbility", 5);
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanInteract() && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            string text = "Press Interact to charge the generator";
            string from = "Generador";
            DialogueSystem.Instance.AddDialogue(text, from);
        }
    }

    public override bool CanInteract()
    {
        return !running;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void SpawnSpecialAbility()
    {
        animator.SetTrigger("Charged");
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorSpawn);
        GameObject core = Instantiate(energyCore, spawnPointPos, spawnPointQuat);
        EnergyCoreController coreParameters = core.GetComponent<EnergyCoreController>();
        coreParameters.stance = PlayerStanceType.STANCE_RED;
    }
}
