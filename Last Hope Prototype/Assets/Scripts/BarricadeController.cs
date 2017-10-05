using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour {

    public LayerMask layersToCollideWith;
    public bool broke = false;
    [HideInInspector]
    public Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (broke)
        {
            anim.SetTrigger("Break");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (layersToCollideWith == (layersToCollideWith | (1 << other.gameObject.layer)) && other.tag == "RedAttack")
        {
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_BreakEnvironment_BreakBarricade);
            this.GetComponent<BoxCollider>().isTrigger = true;
            anim.SetTrigger("Break");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ControllerEvents>().AddRumble(0.4f, new Vector2(0.5f, 0.3f), 0.2f);
        }
    }

    void Break()
    {
        Destroy(this.gameObject);
    }
}
