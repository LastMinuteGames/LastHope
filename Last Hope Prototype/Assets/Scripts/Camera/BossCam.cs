using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{

    [SerializeField]
    private Transform targetT;
    [SerializeField]
    private float moveSpeed = 20f;
    [SerializeField]
    private float tiltAngle;

    private Transform camT;
    private Transform pivotT;
    private Transform bossT;
    private Vector3 pivotEulers;
    private Vector3 dirToBoss;


    private void Awake()
    {
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
        pivotEulers = pivotT.rotation.eulerAngles;
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        bossT = GameObject.FindGameObjectWithTag("Boss").transform;

        transform.localRotation = Quaternion.Euler(0, -90, 0);

        targetT = GameObject.FindGameObjectWithTag("Player").transform;
        pivotT.localRotation = Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z);
    }

    void Update()
    {
        HandleRotationMovement();
    }


    void FixedUpdate()
    {
        dirToBoss = bossT.position - transform.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void HandleRotationMovement()
    {
        transform.localRotation = Quaternion.LookRotation(dirToBoss, Vector3.up);
    }


    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetT.position, Time.deltaTime * moveSpeed);
    }

}
