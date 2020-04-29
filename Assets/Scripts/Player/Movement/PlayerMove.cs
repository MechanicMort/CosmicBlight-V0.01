using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float raycastDistance;

    private Rigidbody rb;

    public LayerMask enemyLayer;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, 50f, enemyLayer);
            int i = 0;

            while (i < enemies.Length)
            {
                enemies[i].gameObject.GetComponent<EnemyAI>().playerDetected = true;
                i++;
            }
        }
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // added Time.deltaTime helps with frame rate and movement issues(Cameron)
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
