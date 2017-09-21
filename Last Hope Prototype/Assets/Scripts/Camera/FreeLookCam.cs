using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCam : MonoBehaviour
{
    [SerializeField]
    private bool invertHorizontalAxis = false;
    [SerializeField]
    private bool invertVerticalAxis = true;

    [Range(0f, 1f)]
    [SerializeField]
    private float moveSmooth = 0.95f;
    [Range(0f, 50)]
    [SerializeField]
    private float maxMoveSpeed = 10f;

    [Range(0f, 10f)]
    [SerializeField]
    private float turnSpeedH = 2f;
    [Range(0f, 10f)]
    [SerializeField]
    private float turnSpeedV = 1f;
    [Range(0f, 1f)]
    [SerializeField]
    private float hSmooth = 0.5f;
    [Range(0f, 1f)]
    [SerializeField]
    private float vSmooth = 0.5f;

    [SerializeField]
    private bool fixedTilt = true;
    [SerializeField]
    private float tiltAngle = 25;
    [SerializeField]
    private float tiltMax = 75f;
    [SerializeField]
    private float tiltMin = 10f;

    [SerializeField]
    private float lockedCamDistance = 15;

    private Transform rigTargetT;
    private Transform pivotT;
    private Transform camT;

    private float lookAngle;

    private Vector3 pivotEulers;
    private Quaternion pivotTargetRot;
    private Quaternion rigTargetRot;

    private int hAxis, vAxis;

    private bool lockMode = false;

    private CameraCollision camCollision;

    //private variables only-for lockmode
    private Transform lockTargetT;
    Vector3 dirToBoss;
    Vector3 pivotDir;
    Vector3 rigDir;


    private void Start()
    {
        rigTargetT = GameObject.FindGameObjectWithTag("Player").transform;
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;

        pivotTargetRot = pivotT.transform.localRotation;
        pivotEulers = pivotT.localRotation.eulerAngles;
        rigTargetRot = transform.localRotation;

        ConfigureCameraAxis(invertHorizontalAxis, invertVerticalAxis);

        camCollision = GetComponent<CameraCollision>();
    }

    private void Update()
    {
        if (!lockMode)
        {
            HandleFreeRotationMovement();
        }
        else
        {
            HandleLockRotationMovement();
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, rigTargetT.position, Time.fixedDeltaTime * maxMoveSpeed);
    }

    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, rigTargetT.position, moveSmooth);
    }


    public void ConfigureCameraAxis(bool invertH, bool invertV)
    {
        invertHorizontalAxis = invertH;
        invertVerticalAxis = invertV;
        hAxis = invertHorizontalAxis ? -1 : 1;
        vAxis = invertVerticalAxis ? -1 : 1;
    }

    private void HandleFreeRotationMovement()
    {
        float x = InputManager.RightJoystick().x;
        float y = InputManager.RightJoystick().z;

        if (!fixedTilt)
        {
            tiltAngle -= y * turnSpeedV * Time.deltaTime * vAxis * 100f;
            tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);

        }
        pivotTargetRot = Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z);
        pivotT.localRotation = Quaternion.Lerp(pivotT.localRotation, pivotTargetRot, vSmooth);

        lookAngle += x * turnSpeedH * Time.deltaTime * hAxis * 100f;
        rigTargetRot = Quaternion.Euler(0f, lookAngle, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rigTargetRot, hSmooth);
    }

    private void HandleLockRotationMovement()
    {
        dirToBoss = lockTargetT.position - pivotT.position;
        //Debug.DrawRay(camT.position, dirToBoss, Color.red);

        pivotDir = dirToBoss;
        pivotDir.x = 0;
        rigDir = dirToBoss;
        rigDir.y = 0;

        pivotTargetRot = Quaternion.LookRotation(pivotDir);
        pivotT.localRotation = Quaternion.Lerp(pivotT.localRotation, pivotTargetRot, vSmooth);

        rigTargetRot = Quaternion.LookRotation(rigDir);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rigTargetRot, hSmooth);
    }
}
