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
            DialogueSystem.Instance.NextDialogue();
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
            string text = "Press B to activate";
            string from = "Elevator";
            DialogueSystem.Instance.AddDialogue(text, from);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !activated)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void ActivateElevator()
    {
        moving = true;
    }
}
