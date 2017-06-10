using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : Interactable {

    public GameObject energyCore;
    public Vector3 spawnPointPos;
    public Quaternion spawnPointQuat;

    bool running = false;

    void Start () {
		
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
            //TODO: Hide message
            Debug.Log("Generator charging...");
            Debug.Log("Wait for 5 seconds");
            running = true;
            Invoke("SpawnSpecialAbility", 5);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanInteract() && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Show message
            Debug.Log("Press Interact to charge the generator");
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
        GameObject core = Instantiate(energyCore, spawnPointPos, spawnPointQuat);
        EnergyCoreController coreParameters = core.GetComponent<EnergyCoreController>();
        coreParameters.stance = PlayerStanceType.STANCE_RED;
    }
}
