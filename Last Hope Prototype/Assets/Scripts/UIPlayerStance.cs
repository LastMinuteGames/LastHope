using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStance : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateUIStanceColor();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateUIStanceColor();
    }

    void UpdateUIStanceColor()
    {
        Color color = new Color();
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().stance)
        {
            case (PlayerStance.STANCE_GREY):
                color.r = 0.5f;
                color.g = 0.5f;
                color.b = 0.5f;
                color.a = 1;
                break;
            case (PlayerStance.STANCE_RED):
                color.r = 1;
                color.g = 0;
                color.b = 0;
                color.a = 1;
                break;
        }
        GetComponent<Image>().color = color;
    }
}
