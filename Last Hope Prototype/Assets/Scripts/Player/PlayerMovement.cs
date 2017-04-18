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
    }


    void Update()
    {
    }


}
