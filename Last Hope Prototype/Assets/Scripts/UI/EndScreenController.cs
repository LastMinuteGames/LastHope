using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

    public GameObject titleText;
    public GameObject text;
    public GameObject continueText;
    [SerializeField]
    private float continueCountdown = 2.5f;
    private float startTime;
    private float currentTime;
    private bool continueActive = false;

	void Start () {
        startTime = Time.time;
        currentTime = startTime;
        continueText.SetActive(false);

        StartCoroutine(FadeTextToFullAlpha(2.5f, titleText.GetComponent<Text>()));
        StartCoroutine(FadeTextToFullAlpha(4f, text.GetComponent<Text>()));
    }
	
	void Update () {
		if (!continueActive && currentTime > startTime + continueCountdown)
        {
            continueActive = true;
            continueText.SetActive(true);
            StartCoroutine(FadeTextToFullAlpha(2f, continueText.GetComponent<Text>()));
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        if (continueActive)
        {
            if (InputManager.Dodge() || Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Menu");
            }
        }
	}

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
