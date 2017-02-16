using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
    public float xTilt = 20;

    Vector3 destination = Vector3.zero;
    PlayerMovement playerMovement;

    float rotateVel = 0f;

    void Start()
    {
        SetCameraTarget(target);
    }

    void SetCameraTarget(Transform t)
    {
        target = t;
        if (target != null)
        {
            if (target.GetComponent<PlayerMovement>())
                playerMovement = target.GetComponent<PlayerMovement>();
            else
                Debug.LogError("Player must have a PlayerMovement script to follow it");
        }
        else
            Debug.LogError("Need a reference to player");
    }

    void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    void MoveToTarget()
    {
        destination = playerMovement.TargetRotation * offsetFromTarget;
        destination += target.position;
        gameObject.transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);
    }
}
