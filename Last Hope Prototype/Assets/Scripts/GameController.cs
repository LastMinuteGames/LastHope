using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public bool isMenu = false;

    //Audio
    new AudioSource audio;
    public AudioClip openFx;
    public AudioClip selectFx;

    void Start () {
        Time.timeScale = 1;
        if(menuInGame != null)
        {
            menuInGame = menuInGame.GetComponent<Canvas>();
            menuInGame.gameObject.SetActive(false);
        }
        isMenu = false;

        //Initialize audio
        audio = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (InputManager.Pause() && !isMenu)
        {
            openMenu();
            audio.PlayOneShot(openFx, 1F);
        }
        else if (InputManager.Pause() && isMenu)
        {
            closeMenu();
            audio.PlayOneShot(openFx, 1F);
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
            audio.PlayOneShot(selectFx, 1F);
        }
        menuInGame.gameObject.SetActive(false);
        Time.timeScale = 1.0F;
        isMenu = false;
    }

}
