using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantController : Interactable {

    public GameObject bridge;
    public GameObject bridgeBlock;
    public Material baseColor;
    public Texture emissiveOff;
    public Texture emissiveOn;
    public Material bridgeBaseColor;
    public Texture bridgeEmissiveOff;
    public Texture bridgeEmissiveOn;

    [SerializeField] private Transform bridgeEnergy;

    private bool running = false;

    // Use this for initialization
    void Start ()
    {
        baseColor.SetTexture("_EmissionMap", emissiveOff);
        bridgeBaseColor.SetTexture("_EmissionMap", bridgeEmissiveOff);
        AudioSources.instance.Play3DAmbientSound((int)AudiosSoundFX.Environment_Bridge_Bridge, bridgeEnergy.position, 0.6f);
    }

    // Update is called once per frame
    void Update () {

	}

    public override void Run()
    {
        if(CanInteract())
        {
            AudioSources.instance.PlayMusic((int)AudiosMusic.BridgeTheme);

            running = true;
            ActivateBridge();
            baseColor.SetTexture("_EmissionMap", emissiveOn);
            bridgeBaseColor.SetTexture("_EmissionMap", bridgeEmissiveOn);
            DialogueSystem.Instance.NextDialogue();
            string text = "Bridge activated";
            string from = "Power Plant";
            DialogueSystem.Instance.AddDialogue(text, from, 2.5f);
        }
    }

    public override bool CanInteract()
    {
        return !running;
    }

    void OnTriggerEnter(Collider other)
    {
        if (running == false && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            string text = "Press B to activate the Energy Bridge";
            string from = "Power Plant";
            DialogueSystem.Instance.AddDialogue(text, from);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !running)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void ActivateBridge()
    {
        bridgeBlock.SetActive(false);
        for (int i = 0; i < bridge.transform.childCount; ++i)
        {
            GameObject child = bridge.transform.GetChild(i).gameObject;
            child.SetActive(true);
        }        
    }
}
