using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : Interactable
{
    private bool activated = false;
    private bool moving = false;
    public GameObject elevator;
    public Transform start;
    public Transform end;

    [SerializeField]
    private float speed = 5.0f;
    private Vector3 velocity;
    private Rigidbody elevatorRig;

    // Use this for initialization
    void Start()
    {
        elevatorRig = elevator.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (moving)
        {
            speed += 0.0005f;
            velocity = Vector3.Lerp(start.position, end.position, speed * Time.deltaTime);
            elevatorRig.MovePosition(velocity);
            Debug.Log(velocity);
            if (elevator.transform.position.y >= end.position.y)
            {
                moving = false;
            }
        }
    }

    public override void Run()
    {
        if (CanInteract())
        {
            activated = true;
            ActivateElevator();
        }
    }

    public override bool CanInteract()
    {
        return !activated;
    }

    void OnTriggerEnter(Collider other)
    {
        if (activated == false && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Show message in screen
            Debug.Log("Press E to activate the elevator.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Hide message
        }
    }

    void ActivateElevator()
    {
        moving = true;
    }
}
