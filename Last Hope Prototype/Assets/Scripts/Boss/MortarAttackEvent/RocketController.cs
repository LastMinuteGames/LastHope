using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public ParticleSystem landingParticles;
    public float heightOffset;
    public float fallDuration;
    public GameObject decal;

    private float destinationY;
    private Vector3 startOffset;
    private float interpolationTime = 0;
    private float currentHeight;
    private float capsuleHeight;

    private bool spawned = false;

    private float timer = 0f;
    private float timeToLive = 1f;

    // Use this for initialization
    void Start () {
        startOffset = new Vector3(0, heightOffset, 0);
        destinationY = gameObject.transform.position.y;
        gameObject.transform.position += startOffset;
        capsuleHeight = GetComponentInParent<CapsuleCollider>().height;
    }
	
	// Update is called once per frame
	void Update () {
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
				AudioSources.instance.PlaySound((int)AudiosSoundFX.Boss_Rocket_Impact);
                StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake(0.3f, 1.75f, 1, 1, this.transform));
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ControllerEvents>().AddRumble(0.4f, new Vector2(0.5f, 0.3f), 0.2f);
                SpawnParticles();
                SpawnDecal();
                spawned = true;
                timer = 0f;

            }
            else
            {
                timer += Time.deltaTime;
                if ((GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().done || timer >= timeToLive))
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
    }

    void SpawnDecal()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Quaternion hitRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
		Vector3 targetPosition = transform.position;
		targetPosition.y += 0.01f;
		Instantiate(decal, targetPosition, hitRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(10);
        }
    }
}
