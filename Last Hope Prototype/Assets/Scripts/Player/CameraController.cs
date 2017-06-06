using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Vector3 pivotOffset = new Vector3(0, 2, 0);      //Offset from player pos to camera focus point (pivot point)
    [SerializeField]
    private Vector3 camOffset = new Vector3(0, 3, -9);       //Offset from pivot to actual camera position
    [Range(0.1f, 1f)]
    [SerializeField]
    private float moveSpeed = 1f;
    [Range(0.0f, 10f)]
    [SerializeField]
    private float turnSpeed = 5f;
    [SerializeField]
    private bool invertHorizontalAxis = false;
    [SerializeField]
    private bool invertVerticalAxis = false;
    [SerializeField]
    private float m_TiltMax = 30f;
    [SerializeField]
    private float m_TiltMin = 30f;

    private Transform playerT;
    private Vector3 lookAt;
    private int hAxis, vAxis;                               // Camera horizontal and vertical axis
    private float m_LookAngle;                              // The rig's y axis rotation.
    private float m_TiltAngle;                              // The pivot's x axis rotation.
    private Quaternion targetRotation;
    private Vector3 totalOffset;
    private Vector3 targetPos;


    void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        lookAt = playerT.position + pivotOffset;
        transform.position = lookAt + camOffset;
        ConfigureCameraAxis(invertHorizontalAxis, invertVerticalAxis);
    }

    public void ConfigureCameraAxis(bool invertH, bool invertV)
    {
        invertHorizontalAxis = invertH;
        invertVerticalAxis = invertV;
        hAxis = invertHorizontalAxis ? -1 : 1;
        vAxis = invertVerticalAxis ? -1 : 1;
    }

    void Update()
    {
        m_LookAngle += Mathf.Clamp(InputManager.RightJoystick().x, -1, 1) * turnSpeed * hAxis;
        m_LookAngle = m_LookAngle % 360;
        m_TiltAngle -= Mathf.Clamp(InputManager.RightJoystick().z, -1, 1) * turnSpeed * vAxis;
        m_TiltAngle = Mathf.Clamp(m_TiltAngle, -30, 30);
    }

    void LateUpdate()
    {
        lookAt = playerT.position + pivotOffset;

        targetRotation = Quaternion.Euler(m_TiltAngle, m_LookAngle, 0);
        transform.rotation = targetRotation;

        totalOffset = targetRotation * camOffset;

        targetPos = lookAt + totalOffset;
        //transform.position = Vector3.Lerp(transform.position, targetPos, Ti * moveSpeed);
        //transform.position = targetPos;

        //Optional line of code. To make sure camera is in the right angle
        //transform.LookAt(playerT.position + pivotOffset);
    }
}
