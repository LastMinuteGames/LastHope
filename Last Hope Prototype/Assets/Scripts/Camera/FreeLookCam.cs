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
    [SerializeField] private float vSmooth = 0.5f;
    [SerializeField] private float hSmooth = 0.5f;


    private Transform camT;
    private Transform pivotT;
    private float lookAngle;
    [SerializeField]
    private float tiltAngle;
    private Vector3 pivotEulers;
    private Quaternion pivotTargetRot;
    private Quaternion transformTargetRot;
    private int hAxis, vAxis;
    public float y = 0;


    private void Awake ()
    {
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
        pivotEulers = pivotT.localRotation.eulerAngles;
        pivotTargetRot = pivotT.transform.localRotation;
        transformTargetRot = transform.localRotation;
        ConfigureCameraAxis(invertHorizontalAxis, invertVerticalAxis);
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        targetT = GameObject.FindGameObjectWithTag("Player").transform;
        pivotT.localRotation = Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z);
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

    private void LateUpdate ()
    {
        FollowTarget();
    }

    private void HandleRotationMovement ()
    {
        var x = InputManager.RightJoystick().x;
        var y = InputManager.RightJoystick().z;

        lookAngle += x * turnSpeed * Time.deltaTime* hAxis * 100f;

        transformTargetRot = Quaternion.Euler(0f, lookAngle, 0f);

        //tiltAngle -= y * turnSpeed * Time.deltaTime * vAxis * 100f;
        //tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);

        //pivotTargetRot = Quaternion.Lerp(pivotTargetRot, Quaternion.Euler(tiltAngle, pivotEulers.y, pivotEulers.z), vSmooth);

        //pivotT.localRotation = pivotTargetRot;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, transformTargetRot, hSmooth);

    }

    private void FollowTarget ()
    {
        transform.position = Vector3.Lerp(transform.position, targetT.position, Time.deltaTime * moveSpeed);
    }


}
