using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public bool isMenu = false;
    
	void Start () {
        Time.timeScale = 1;
        if(menuInGame != null)
        {
            menuInGame = menuInGame.GetComponent<Canvas>();
            menuInGame.gameObject.SetActive(false);
        }
        isMenu = false;
    }
	
	void Update () {
        if (InputManager.Pause() && !isMenu)
        {
            openMenu();
        }
        else if (InputManager.Pause() && isMenu)
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
