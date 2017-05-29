using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Canvas menuInGame;
    public bool isMenu = false;
    public Camera camera;
    
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

        if (InputManager.Interact())
        {
            Debug.Log("Hola");
            StartCoroutine(Shake()); 
            Debug.Log("Adios");
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

    IEnumerator Shake(float duration = 0.1f, float magnitude = 1f)
    {

        float elapsed = 0.0f;

        //Vector3 originalCamPos = Camera.main.transform.position;
        Vector3 originalCamPos = camera.transform.position;
        Debug.Log("Dentro");
        Debug.Log(originalCamPos);

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }

}
