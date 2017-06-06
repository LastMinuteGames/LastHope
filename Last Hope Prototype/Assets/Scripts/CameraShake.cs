using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    new Camera camera;
    public bool done = false;
    private bool shaking = false;
    public float distanceShake = 40f;
    public Transform player;
    private Vector3 offsetToPivot;


    void Awake()
    {
        offsetToPivot = transform.localPosition;
        
    }

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

    public IEnumerator Shake(float duration = 0.2f, float magnitude = 0.5f, float xMultiplier = 1.0f, float yMultiplier = 1.0f, Transform positionCalled = null)
    {
        if (!shaking && CanShake(positionCalled))
        {
            done = false;
            shaking = true;
            if (this.GetComponent<CameraController>())
            {
                this.GetComponent<CameraController>().enabled = false;
            }
            float elapsed = 0.0f;

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

                transform.localPosition = new Vector3(offsetToPivot.x + x, offsetToPivot.y + y, offsetToPivot.z);

                yield return null;
            }

            transform.localPosition = offsetToPivot;
            if (this.GetComponent<CameraController>())
            {
                this.GetComponent<CameraController>().enabled = true;
            }
            done = true;
            shaking = false;

        }
        
    }

    public bool CanShake(Transform positionCalled)
    {
        if (!positionCalled)
        {
            return true;
        }
        else
        {
            float distance = Vector3.Distance(positionCalled.position, player.position);
            if (distance <= distanceShake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
