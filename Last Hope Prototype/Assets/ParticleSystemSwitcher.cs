using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSwitcher : MonoBehaviour
{

    private List<GameObject> particles = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            particles.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activate(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Activate(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Activate(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Activate(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Activate(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Activate(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Activate(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Activate(8);
        }
    }

    void Activate(int num)
    {
        foreach (GameObject particle in particles)
        {
            particle.GetComponent<ParticleSystem>().Stop();
            particle.gameObject.SetActive(false);
        }
        particles[num - 1].gameObject.SetActive(true);
        particles[num - 1].GetComponent<ParticleSystem>().Play();
    }
}
