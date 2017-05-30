using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    public ParticleSystem explosion;
    public GameObject decal;

    private float capsuleHeight;

    void Start () {
        Invoke("Die", 3);
        capsuleHeight = GetComponentInParent<CapsuleCollider>().height;
    }
	
    void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        SpawnDecal();
        Destroy(gameObject);
    }

    void SpawnDecal()
    {
        float verticalRotation = Random.Range(0, 360);
        Quaternion hitRotation = Quaternion.Euler(90, verticalRotation, 0);
        float verticalPosition = capsuleHeight / 2 * transform.localScale.y - 0.001f;
        GameObject a = Instantiate(decal, transform.position - new Vector3(0, verticalPosition, 0), hitRotation);
    }
}
