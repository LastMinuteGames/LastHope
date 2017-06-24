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
        /*if (canSpawn)
        {
            if (InputManager.Interact())
            {
                spawned = true;
                Debug.Log("Generator charging...");
                Debug.Log("Wait for 5 seconds");
                canSpawn = false;
                Invoke("SpawnSpecialAbility", 5);
            }
        }*/
    }

    public override void Run()
    {
        if(CanInteract())
        {
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_PlayerToWorld_Interact);
            animator.SetTrigger("Charging");
            //TODO: Hide message
            Debug.Log("Generator charging...");
            Debug.Log("Wait for 5 seconds");
            running = true;
            Invoke("SpawnSpecialAbility", 5);
            //Dialogue
            string[] a = new string[1];
            a[0] = "El generador se activará en 5 segundos";
            string[] b = new string[1];
            b[0] = "Generador";
            DialogueSystem.Instance.AddNewDialogue(a, b);
            DialogueSystem.Instance.ShowDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanInteract() && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Show message
            Debug.Log("Press Interact to charge the generator");
            if (!DialogueSystem.Instance.show)
            {
                string[] a = new string[1];
                a[0] = "Press Interact to charge the generator";
                string[] b = new string[1];
                b[0] = "Generator";
                DialogueSystem.Instance.AddNewDialogue(a, b);
                DialogueSystem.Instance.ShowDialogue();
            }
            
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
            //TODO: Hide messages
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
