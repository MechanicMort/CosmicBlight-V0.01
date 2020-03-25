using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    public Transform playerBody;
    private float xRotation = 0f;
    public Transform _Pivot;
    public float speed = 100f;
    public float maxAngle = 20f;
    public GameObject locationR;
    public GameObject locationL;
    public GameObject startLocation;
    public GameObject targetLocation;

    float curAngle = 0f;


 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (_Pivot == null && transform.parent != null) _Pivot = transform.parent;
    }

    private void Update()
    {
        MouseMovement();
        Lean();
        
    }

    private void Lean()
    {
        if (Input.GetKey(KeyCode.Q) && !canLeanLeft())
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, maxAngle, speed * Time.deltaTime);
            targetLocation = locationL;
        }
        else if (Input.GetKey(KeyCode.E) && !canLeanRight())
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, -maxAngle, speed * Time.deltaTime);
            targetLocation = locationR;
        }
        else
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, 0f, speed * Time.deltaTime);
            targetLocation = startLocation;
        }

        if (!canLeanLeft() || !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            transform.localRotation = Quaternion.AngleAxis(curAngle, Vector3.forward);
            transform.position = (Vector3.MoveTowards(transform.position, targetLocation.transform.position, 0.1f));
        }

    }


    private bool canLeanLeft()
    {
        Debug.DrawRay(transform.position, Vector3.left, Color.red,0.3f);
        return Physics.Raycast(transform.position, Vector3.left, 0.3f,8);
    }
    private bool canLeanRight()
    {
        Debug.DrawRay(transform.position, -Vector3.left, Color.red, 0.3f);
        return Physics.Raycast(transform.position, -Vector3.left, 0.3f,8);
    }

    private void MouseMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

}
