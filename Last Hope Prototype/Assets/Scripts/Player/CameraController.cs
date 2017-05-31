using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerT;

    //Offset from player pos to camera focus point (pivot point)
    public Vector3 pivotOffset = new Vector3(0, 2, 0);

    //Offset from pivot to actual camera position
    public Vector3 camOffset = new Vector3(0, 3, -9);
    public float camSpeed = 350f;

    private float h, v;

    //Point at which camera will be looking at
    private Vector3 lookAt;

    void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        lookAt = playerT.position + pivotOffset;
        transform.position = lookAt + camOffset;
    }

    void Update()
    {
        h += Mathf.Clamp(InputManager.RightJoystick().x, -1, 1) * camSpeed * Time.deltaTime;
        h = h % 360;
        v -= Mathf.Clamp(InputManager.RightJoystick().z, -1, 1) * camSpeed * Time.deltaTime;
        v = Mathf.Clamp(v, -30, 30);
    }

    void LateUpdate()
    {
        lookAt = playerT.position + pivotOffset;

        Quaternion targetRotation = Quaternion.Euler(v, h, 0);
        transform.rotation = targetRotation;

        Vector3 totalOffset = targetRotation * camOffset;

        transform.position = lookAt + totalOffset;

        //Optional line of code. To make sure camera is in the right angle
        //transform.LookAt(playerT.position + pivotOffset);
    }
}
