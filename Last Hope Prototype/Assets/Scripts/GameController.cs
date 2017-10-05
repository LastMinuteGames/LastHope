using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LastHope.SoundManager;

public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public InGameMenu inGame;
    public bool isMenu = false;

    //Audio
    private int pauseFxId;
    private int unpauseFxId;
    private int applySelectionFxId;



    void Start ()
    {
        Time.timeScale = 1;
        if(menuInGame != null)
        {
            menuInGame = menuInGame.GetComponent<Canvas>();
            menuInGame.gameObject.SetActive(false);
        }
        isMenu = false;

        if (!AudioSources.instance)
        {
            Debug.LogWarning("PUT AUDIOSOURCES PREFAB IN SCENE!");
        }

        pauseFxId = (int)AudiosSoundFX.Menu_Pause;
        unpauseFxId = (int)AudiosSoundFX.Menu_Unpause;
        applySelectionFxId = (int)AudiosSoundFX.Menu_ApplySelection;

    }
	
	void Update () {
        if (InputManager.Pause() && !isMenu)
        {
            openMenu();
            AudioSources.instance.PlaySound(pauseFxId);
        }
        else if (InputManager.Pause() && isMenu)
        {
            
            closeMenu();
            AudioSources.instance.PlaySound(unpauseFxId);
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
        if (hasAudio)
        {
            AudioSources.instance.PlaySound(applySelectionFxId);
        }
        menuInGame.gameObject.GetComponent<InGameMenu>().isConfirmExit = false;
        inGame.closeExitMenu();
        //menuInGame.gameObject.GetComponent<InGameMenu>().confirmExit.gameObject.SetActive(false);
        menuInGame.gameObject.SetActive(false);
        Time.timeScale = 1.0F;
        isMenu = false;
    }

}
