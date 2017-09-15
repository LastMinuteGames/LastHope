using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private bool activated = false;
    private bool moving = false;
    public GameObject elevator;
    public Transform start;
    public Transform end;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject elevatorWalls;

    private Vector3 velocity;
    private Rigidbody elevatorRig;

    private MainCameraManager mainCameraManager;

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        elevatorRig = elevator.GetComponent<Rigidbody>();
        mainCameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraManager>();
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
            if (elevator.transform.position.y >= end.position.y-0.1f)
            {
                moving = false;
                elevatorWalls.SetActive(false);
                anim.SetBool("running", false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (activated == false && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("running", true);
            activated = true;
            ActivateElevator();
        }
    }
    

    void ActivateElevator()
    {
        mainCameraManager.SetBossCam();
        moving = true;
        elevatorWalls.SetActive(true);
    }
}
