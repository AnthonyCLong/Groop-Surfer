using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject focalPoint;
    public float sensitivity;

    public void Start() {
        transform.LookAt(focalPoint.transform.position);
    }

    public void Orbit() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.RotateAround(focalPoint.transform.position, Vector3.up, mouseX * sensitivity);
        transform.RotateAround(focalPoint.transform.position, transform.right, -mouseY * sensitivity);
        transform.LookAt(focalPoint.transform.position);
    }

    public void Zoom() {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float desiredSize = transform.gameObject.GetComponent<Camera>().orthographicSize - scrollWheel * sensitivity;
        transform.gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(desiredSize, 0.1f, 100);
    }

    public void Pan() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 moveX = transform.right * mouseX;
        Vector3 moveY = transform.up * mouseY;

        focalPoint.transform.position -= moveX + moveY;
        transform.position -= moveX + moveY;
    }
}
