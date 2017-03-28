using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 3;
    public float speed = 5;
    public Transform camT;
    public Vector3 movement;
    public Vector3 targetDirection;

    private float h, v;
    private Rigidbody rigidBody;
    private Vector3 camForward;
    private Vector3 camRight;


    void Start()
    {
        camT = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;
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
