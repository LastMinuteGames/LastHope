using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAxisRotation : MonoBehaviour {
    private bool rotating = false;
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            rotating = !rotating;
        }
        if (rotating)
        {
            transform.Rotate(0, 0.5f, 0);
        }
	}
}
