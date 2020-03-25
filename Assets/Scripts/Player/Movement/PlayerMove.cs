using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leanSpeed;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float raycastDistance;
    [SerializeField] private float sprintMod;
    [SerializeField] private float crouchMod;


    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouchMod = 0.5f;
            transform.localScale = new Vector3(1f,0.5f,1f);
        }
        else
        {
            crouchMod = 1f;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMod = 1.5f;
        }
        else
        {
            sprintMod = 1f;
        }
    }

   

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        moveSpeed = baseMoveSpeed * crouchMod * sprintMod;
        Vector3 movement = new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }


}
