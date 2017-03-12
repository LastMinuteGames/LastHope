using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public bool isMenu = false;

	// Use this for initialization
	void Start () {
        menuInGame = menuInGame.GetComponent<Canvas>();
        menuInGame.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenu)
        {
            openMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenu)
        {
            closeMenu();
        }
    }

    public void openMenu()
    {
        menuInGame.gameObject.SetActive(true);
        Time.timeScale = 0.0F;
        isMenu = true;
    }

    public void closeMenu()
    {
        menuInGame.gameObject.SetActive(false);
        Time.timeScale = 1.0F;
        isMenu = false;
    }
}
