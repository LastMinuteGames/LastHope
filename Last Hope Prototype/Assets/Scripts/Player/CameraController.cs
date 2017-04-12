using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform playerT;
    //public Vector3 pivotOffset = new Vector3(0, 1, 0);
    public Vector3 pivotOffset = new Vector3(4, 1, 4);
    public Vector3 camOffset = new Vector3(0, 2, 3);
    public float camSpeed = 200f;

    private Transform cam;
    private float h, v;

    void Awake()
    {
        cam = transform;
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        cam.position = playerT.position + pivotOffset + camOffset;
    }

    void Update()
    {
        //h += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * camSpeed * Time.deltaTime;
        //v = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * camSpeed * Time.deltaTime;
        h += Mathf.Clamp(InputManager.RightJoystick().x, -1, 1) * camSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, h, 0);
        cam.rotation = targetRotation;
        Vector3 totalOffset = pivotOffset + camOffset;
        totalOffset = targetRotation * totalOffset;
        Debug.DrawRay(playerT.position, totalOffset);

        cam.position = playerT.position + totalOffset;

        cam.rotation.SetLookRotation(playerT.position);
        transform.LookAt(playerT.position + pivotOffset);
    }
}
