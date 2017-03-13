using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour {

    PlayerController playerController;
    PlayerEnergy playerEnergy;

	// Use this for initialization
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergy>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerEnergy.LoseEnergy(1))
            {
                switch (playerController.stance)
                {
                    case (PlayerStance.NEUTRAL):
                        Debug.Log("NEUTRAL SPECIAL ATTACK!");
                        break;
                    case (PlayerStance.RED):
                        Debug.Log("RED SPECIAL ATTACK!");
                        break;
                }
            }
        }
	}
}
