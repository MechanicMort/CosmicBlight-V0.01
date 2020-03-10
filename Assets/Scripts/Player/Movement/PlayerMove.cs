using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float moveSpeed;
    private float crouchedSpeedMod;
    private float sprintSpeedMod;
    [SerializeField] private float standingHeight;
    [SerializeField] private float crouchingHeight;
    [SerializeField] private float raycastDistance;
    private bool isCrouched;
    private bool isSprinting;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprintSpeedMod = 1f;
        crouchedSpeedMod = 1f;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        Jump();
        Crouch();
        Sprint();
    }


    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isSprinting)
            {
                isSprinting = false;
                sprintSpeedMod = 1f;
            }
            else
            {
                isSprinting = true;
                sprintSpeedMod = 1.5f;
            }
        }
    }

    //Added crouch(Cameron)
   private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C)|| Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouched)
            {
                isCrouched = false;
                transform.localScale =  new Vector3(1,standingHeight,1);
                crouchedSpeedMod = 1f;
            }
            else
            {
                isCrouched = true;
                transform.localScale = new Vector3(1, crouchingHeight, 1);
                crouchedSpeedMod = 0.5f;
            }
        }
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // added Time.deltaTime helps with frame rate and movement issues(Cameron)
        moveSpeed = baseSpeed * sprintSpeedMod * crouchedSpeedMod;
        print(sprintSpeedMod);
        print(crouchedSpeedMod);
        Vector3 movement = new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCrouched == false)
        {
            if (isGrounded())
            {
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            }
        }
    }

    private bool isGrounded()
    {
        //Added a layermask exclusion so only counts objects on layer 8(Cameron)
        LayerMask layer = ~8;
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, layer);
    }


}
