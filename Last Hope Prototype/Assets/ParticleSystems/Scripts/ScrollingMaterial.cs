using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMaterial : MonoBehaviour {

    public float xScrollSpeed = 0.5f;
    public float yScrollSpeed = 0;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void FixedUpdate()
    {
        float offsetX = Time.time * xScrollSpeed;
        float offsetY = Time.time * yScrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
