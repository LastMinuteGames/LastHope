using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public ParticleSystem landingParticles;
    public float heightOffset;
    public float fallDuration;
    public GameObject enemy;
    public GameObject decal;

    private float destinationY;
    private Vector3 startOffset;
    private float interpolationTime = 0;
    private float currentHeight;
    private float capsuleHeight;

    private bool spawned = false;

    void Start()
    {
        startOffset = new Vector3(0, heightOffset, 0);
        destinationY = gameObject.transform.position.y;
        gameObject.transform.position += startOffset;
        capsuleHeight = GetComponentInParent<CapsuleCollider>().height;
    }

    void Update()
    {
        currentHeight = Mathf.Lerp(startOffset.y, destinationY, interpolationTime);
        if (interpolationTime <= 1)
        {
            interpolationTime += Time.deltaTime / fallDuration;
        }
        Vector3 currentPos = new Vector3(gameObject.transform.position.x, currentHeight, gameObject.transform.position.z);
        gameObject.transform.position = currentPos;
        if (currentHeight <= destinationY)
        {
            if (!spawned)
            {
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(0.3f, 1.75f));
                SpawnParticles();
                SpawnDecal();
                spawned = true;

            }else
            {
                if (GameObject.Find("Main Camera").GetComponent<CameraShake>().done)
                {
                    Destroy(gameObject);
                }
            }

        }
    }

    void SpawnParticles()
    {
        Vector3 landingPos = transform.position;
        Instantiate(landingParticles, transform.position, transform.rotation);
        Instantiate(enemy, landingPos, transform.rotation);
    }

    void SpawnDecal()
    {
        float verticalRotation = Random.Range(0, 360);
        Quaternion hitRotation = Quaternion.Euler(90, verticalRotation, 0);
        float verticalPosition = capsuleHeight / 2 * transform.localScale.y - 0.001f;
        Instantiate(decal, transform.position - new Vector3(0, verticalPosition, 0), hitRotation);
    }
}