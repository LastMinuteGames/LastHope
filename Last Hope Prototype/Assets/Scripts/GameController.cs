using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LastHope.SoundManager;

public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public bool isMenu = false;

    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource swapSelection;

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
            pause.PlaySound(pause.clip, 1f);
        }
        else if (InputManager.Pause() && isMenu)
        {
            closeMenu();
            unpause.PlaySound(unpause.clip, 1f);

        }
    }

    public void openMenu()
    {
        menuInGame.gameObject.SetActive(true);
        Time.timeScale = 0.0F;
        isMenu = true;
    }

    public void closeMenu(bool hasAudio = false)
    {
        if (hasAudio) {
            swapSelection.PlaySound(swapSelection.clip, 1f);
        }
        menuInGame.gameObject.SetActive(false);
        Time.timeScale = 1.0F;
        isMenu = false;
    }

}
