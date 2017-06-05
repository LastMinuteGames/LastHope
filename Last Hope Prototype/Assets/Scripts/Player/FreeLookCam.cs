using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCam : MonoBehaviour
{
    [SerializeField] private Transform targetT;
    [SerializeField] private float moveSpeed = 20f;
    [Range(0f, 10f)] [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float tiltMax = 75f;
    [SerializeField] private float tiltMin = 10f;
    [SerializeField] private bool invertHorizontalAxis = false;
    [SerializeField] private bool invertVerticalAxis = true;

    private Transform camT;
    private Transform pivotT;
    private float lookAngle;
    private float tiltAngle;
    private Vector3 pivotEulers;
    private Quaternion pivotTargetRot;
    private Quaternion transformTargetRot;
    private int hAxis, vAxis;


    private void Awake ()
    {
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
        pivotEulers = pivotT.rotation.eulerAngles;
        pivotTargetRot = pivotT.transform.localRotation;
        transformTargetRot = transform.localRotation;
        ConfigureCameraAxis(invertHorizontalAxis, invertVerticalAxis);
    }

    private void Start()
    {
        targetT = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ConfigureCameraAxis(bool invertH, bool invertV)
    {
        invertHorizontalAxis = invertH;
        invertVerticalAxis = invertV;
        hAxis = invertHorizontalAxis ? -1 : 1;
        vAxis = invertVerticalAxis ? -1 : 1;
    }


    private void Update ()
    {
        HandleRotationMovement();
    }

    private void FixedUpdate ()
    {
        FollowTarget(Time.deltaTime);
    }

    private void HandleRotationMovement ()
    {
        var x = InputManager.RightJoystick().x;
        var y = InputManager.RightJoystick().z;

        lookAngle += x * turnSpeed * hAxis;

        transformTargetRot = Quaternion.Euler(0f, lookAngle, 0f);

        tiltAngle -= y * turnSpeed * vAxis;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);

        pivotTargetRot = Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z);

        pivotT.localRotation = pivotTargetRot;
        transform.localRotation = transformTargetRot;

    }

    private void FollowTarget (float deltaTime)
    {
        transform.position = Vector3.Lerp(transform.position, targetT.position, deltaTime * moveSpeed);
    }


}
