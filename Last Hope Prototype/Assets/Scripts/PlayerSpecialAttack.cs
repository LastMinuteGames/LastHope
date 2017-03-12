using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().stance)
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
