using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoreController : MonoBehaviour {
    public PlayerStance color;
    public Material greyMat;
    public Material redMat;
    
    private Renderer rend;

	void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        switch (color)
        {
            case PlayerStance.STANCE_BLUE:
                rend.sharedMaterial = greyMat;
                break;
            case PlayerStance.STANCE_RED:
                rend.sharedMaterial = redMat;
                break;
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            switch (color)
            {
                case PlayerStance.STANCE_BLUE:
                    player.EnableGreyAbility();
                    player.ChangeStance(color);
                    Debug.Log("Grey special ability learned");
                    break;
                case PlayerStance.STANCE_RED:
                    player.EnableRedAbility();
                    player.ChangeStance(color);
                    Debug.Log("Red special ability learned");
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
