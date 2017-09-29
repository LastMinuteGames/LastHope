using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : Interactable {

    public GameObject energyCore;
    public Vector3 spawnPointPos;
    [HideInInspector]
    public Quaternion spawnPointQuat;
    public ParticleSystem particles;

    [SerializeField]
    private GenericCombatEvent combatEvent;
    [SerializeField]
    private PlayerController player;

    public Animator animator;

    public GameObject generator;
    private Material generatorMaterial;
    public Texture emisiveOff;
    public Texture emisiveOn;

    bool running = false;

    void Start () {
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorNoise);
        generatorMaterial = generator.GetComponent<MeshRenderer>().material;
    }
	
	void Update () {

    }

    public override void Run()
    {
        if(CanInteract())
        {
            combatEvent.EventStart(player);
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_PlayerToWorld_Interact);
            animator.SetTrigger("Charging");
            particles.Play(); 
            //TODO: Hide message
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorNoise);
            Debug.Log("Generator charging...");
            running = true;
            animator.SetTrigger("running");
            generatorMaterial.SetTexture("_EmissionMap", emisiveOn);
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanInteract() && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = other.GetComponent<PlayerController>();
            string text = "Press B to charge the Generator";
            string from = "Generator";
            DialogueSystem.Instance.AddDialogue(text, from);
        }
    }

    public override bool CanInteract()
    {
        return !running;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !running)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    public void SpawnSpecialAbility()
    {
        animator.SetTrigger("Charged");
        particles.Stop();
        generatorMaterial.SetTexture("_EmissionMap", emisiveOff);
        animator.ResetTrigger("running");
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorSpawn);
        GameObject core = Instantiate(energyCore, spawnPointPos, spawnPointQuat);
        EnergyCoreController coreParameters = core.GetComponent<EnergyCoreController>();
        coreParameters.stance = PlayerStanceType.STANCE_RED;
        string text = "Generator charged!";
        string from = "Generator";
        DialogueSystem.Instance.AddDialogue(text, from, 2.5f);
    }

    void PlayGeneratorNoise()
    {
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Generator_GeneratorNoise);
    }

    void StopGeneratorNoise()
    {
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_PlayerToWorld_Interact);
    }
}
