using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGod : MonoBehaviour
{
    public float speed = 15;
    public float wheelSpeed = 15;
    public float rotateSpeed = 50;
    private float xDeg, yDeg;
    private float h, v;


    void Start()
    {
        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }


    void Update()
    {

    }

    void LateUpdate()
    {
        if (this.GetComponent<Camera>().enabled == false)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Vector3 wasdMovement = (h * transform.right + v * transform.forward) * Time.deltaTime * speed;
            transform.position += wasdMovement;

            xDeg += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            yDeg -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            if (yDeg < -360)
                yDeg += 360;
            if (yDeg > 360)
                yDeg -= 360;

            transform.rotation = Quaternion.Euler(yDeg, xDeg, 0);
        }

        transform.position += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * wheelSpeed * transform.forward;
        transform.position += new Vector3(0, (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0), 0) * Time.deltaTime * speed;
    }

}
