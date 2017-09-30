using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour {

    [SerializeField] private Transform bossCamTravelT;
	[SerializeField] private float movementSmooth = 0.2f;
    [SerializeField] private float fov = 80f;

    private Animator animator;
    private Transform pivotT;
    private Transform camT;
    private Transform playerT;
    private Vector3 targetPos;

    private bool following = false;


    private void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;

        animator = bossCamTravelT.gameObject.GetComponent<Animator>();
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
    }

    private void Start ()
    {
        camT.parent = bossCamTravelT;
        camT.localPosition = Vector3.zero;
        camT.localRotation = Quaternion.identity;
		camT.GetComponentInChildren<Camera>().fieldOfView = fov;
        Animate();
    }

    public void Animate()
    {
        animator.SetTrigger("Activated");
    }

    public void StartFollowing()
    {
        following = true;
    }

    private void Update()
    {
        if (following)
        {
            targetPos = new Vector3(playerT.position.x, camT.position.y, camT.position.z);
            camT.position = Vector3.Lerp(camT.position, targetPos, movementSmooth);
        }
    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }




}
