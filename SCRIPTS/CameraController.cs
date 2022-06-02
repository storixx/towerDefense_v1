using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
     [Header("Screen Attributes")]   
    public float screenBorderThickness = 10f; 
    
    [Header("Camera Attributes")]
    public float cameraViewSpeed = 30f;

    public float zoomInLimit = 15f;
    public float zoomOutLimit = 70f;

    public float xRotationLowerLimit = 10f;
    public float xRotationUpperLimit = 70f;

    public float cameraYRot = 180f;
    public float cameraZRot;

    public bool allowMovement = true;

    private void Update()
    {
        if (GameMananger.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        
        if (Input.GetKeyDown("q"))
        {
            allowMovement = !allowMovement;
        }

        if (!allowMovement)
        {
            return;
        }  
        
        // w <--> d
        // s <--> a
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - screenBorderThickness)
        {
            transform.Translate(Vector3.back * cameraViewSpeed * Time.deltaTime, Space.World); // forward
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= screenBorderThickness)
        {
            transform.Translate(Vector3.forward * cameraViewSpeed * Time.deltaTime, Space.World); // back
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - screenBorderThickness)
        {
            transform.Translate(Vector3.left * cameraViewSpeed * Time.deltaTime, Space.World); // right
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= screenBorderThickness)
        {
            transform.Translate(Vector3.right * cameraViewSpeed * Time.deltaTime, Space.World); // left
        }

        if ((Input.GetAxis("Mouse ScrollWheel") > 0f) && transform.position.y > zoomInLimit)
        {

            transform.Translate(Vector3.down * 300f * Time.deltaTime, Space.World); // zoom in

            if (transform.position.y > xRotationLowerLimit && transform.position.y < xRotationUpperLimit)
                transform.rotation = Quaternion.Euler(transform.position.y, cameraYRot, cameraZRot);
        }

        if ((Input.GetAxis("Mouse ScrollWheel") < 0f) && transform.position.y < zoomOutLimit)
        {
            transform.Translate(Vector3.up * 300f * Time.deltaTime, Space.World); // zoom out

            if (transform.position.y > xRotationLowerLimit && transform.position.y < xRotationUpperLimit)
                transform.rotation = Quaternion.Euler(transform.position.y, cameraYRot, cameraZRot);
        }
    }
}