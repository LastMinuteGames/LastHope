using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 3;
    public float speed = 5;
    public Transform camT;

    private float h, v;
    private Vector3 targetDirection;
    private Rigidbody rigidBody;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movement;


    void Start()
    {
        camT = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        camForward = camT.TransformDirection(Vector3.forward);
        camForward.y = 0;
        camForward.Normalize();

        Debug.DrawRay(transform.position, camForward, Color.black);
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);

        camRight = new Vector3(camForward.z, 0, -camForward.x);
        Debug.DrawRay(transform.position, camRight, Color.red);

        targetDirection = camForward * v + camRight * h;
        Debug.DrawRay(transform.position, targetDirection, Color.blue);

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }


    void Move()
    {
        movement = rigidBody.velocity;

        movement.z = 0;
        v = Mathf.Abs(v);
        h = Mathf.Abs(h);
        float totalImpulse = h + v;
        totalImpulse = (totalImpulse > 1) ? 1 : totalImpulse;
        movement.z += totalImpulse * speed;

        movement.x = 0;

        if (movement.y > 2)
            movement.y = 2;

        rigidBody.velocity = transform.TransformDirection(movement);
    }

}
