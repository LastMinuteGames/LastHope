using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    Camera camera;
    public bool done = false;

    // Use this for initialization
    void Start () {
        camera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (InputManager.Interact())
        {
            StartCoroutine(Shake());
        }*/
    }

    public IEnumerator Shake(float duration = 0.2f, float magnitude = 0.5f, float xMultiplier = 1.0f, float yMultiplier = 1.0f)
    {
        done = false;
        if (this.GetComponent<CameraController>())
        {
            Debug.Log("A false!");
            this.GetComponent<CameraController>().enabled = false;
        }
        float elapsed = 0.0f;

        //Vector3 originalCamPos = Camera.main.transform.position;
        Vector3 originalCamPos = camera.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper * xMultiplier;
            y *= magnitude * damper * yMultiplier;

            Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

            yield return null;
        }
 
        Camera.main.transform.position = originalCamPos;
        if (this.GetComponent<CameraController>())
        {
            Debug.Log("A true!");
            this.GetComponent<CameraController>().enabled = true;
        }
        done = true;
    }

}
