using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float maxSpeed = 5.0f;
    public float smoothTime = 10F;
    public GameObject elevatorWalls;
    public Animator elevatorAC;

    private Transform elevatorRig;
    private MainCameraManager mainCameraManager;
    private Rigidbody elevatorRigRB;

    private bool activated = false;
    private bool moving = false;
    private Vector3 currentVelocity;


    void Awake()
    {
        elevatorRig = transform.parent;
        mainCameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraManager>();
        elevatorRigRB = elevatorRig.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (!moving)
        {
            return;
        }

        Vector3 targetPos = Vector3.SmoothDamp(elevatorRig.position, end.position, ref currentVelocity, smoothTime, maxSpeed);

        //speed += 0.0005f;
        //currentVelocity = Vector3.Lerp(start.position, end.position, speed * Time.deltaTime);

        elevatorRigRB.MovePosition(targetPos);
        float speedproportion = currentVelocity.magnitude / maxSpeed;
        elevatorAC.speed = speedproportion;

        if (elevatorRig.position.y >= end.position.y - 0.1f)
        {
            elevatorAC.SetBool("running", false);
            moving = false;
            elevatorWalls.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (activated == false && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ActivateElevator();
        }
    }

    void ActivateElevator()
    {
        elevatorAC.SetBool("running", true);
        mainCameraManager.SetBossCam();
        activated = true;
        moving = true;
        elevatorWalls.SetActive(true);
    }
}
