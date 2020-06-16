using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    float CameraZoomDis;
    [SerializeField]
    float RotationSpeed = 5.0f;
    [SerializeField]
    public Transform playerTransform;
    [SerializeField]
    float smoothFactor = 0.5f;
    private Vector3 cameraOffset;
    bool RotateAroundPlay = false;

    private void Awake()
    {
        cameraOffset = transform.position - playerTransform.position;
        RotateAroundPlay = false;
    }


    private void Update()
    {
        Vector3 newPos = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);


        Zoom();
        Rotation();

        if ( RotateAroundPlay)
        {
            transform.LookAt(playerTransform);
        }
    }

    void Rotation()
    {
        //rotation
        if (Input.GetMouseButtonDown(1))
        {
            RotateAroundPlay = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            RotateAroundPlay = false;
        }
        if (RotateAroundPlay)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            cameraOffset = camTurnAngle * cameraOffset;
        }
        
    }

    void Zoom()
    {
        //zoom in and out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView -= CameraZoomDis;
            //GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y-CameraZoomDisY, transform.position.z + CameraZoomDisZ);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView += CameraZoomDis;
            //GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + CameraZoomDisY, transform.position.z - CameraZoomDisZ);
        }
    }


}
