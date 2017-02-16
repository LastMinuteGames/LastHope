using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 15;
        public float horizontalVel = 15;
        public float rotatVel = 100f;
        public float distToGround = 0.5f;
        public float maxFallingSpeed = -80f;
        public LayerMask ground;
    }
    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    private Vector3 movement = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;
    private Rigidbody rigidBody;
    private float forwardInput, horizontalInput, turnInput;
    private bool isGrounded;


    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
        forwardInput = horizontalInput = turnInput = 0;
        isGrounded = true;
    }

    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        //CheckGrounded();
        Move();
    }

    void GetInput()
    {
        forwardInput = Input.GetAxisRaw("Vertical");
        horizontalInput = (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0);
        turnInput = Input.GetAxis("Horizontal");
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotatVel * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground);
    }

    void Move()
    {
        movement = rigidBody.velocity;

        if (Mathf.Abs(forwardInput) > 0)
            movement.z = moveSetting.forwardVel * forwardInput;
        else
            movement.z = 0;

        if (Mathf.Abs(horizontalInput) > 0)
            movement.x = moveSetting.horizontalVel * horizontalInput;
        else
            movement.x = 0;

        if (movement.y > 2)
            movement.y = 2;


        rigidBody.velocity = transform.TransformDirection(movement);
    }




}
