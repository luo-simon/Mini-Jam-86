using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public SpawnManager spawnManager;
    public float jumpForce;

    [SerializeField] private bool grounded;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += transform.right * Time.deltaTime * speed * h;
        transform.position += transform.forward * Time.deltaTime * speed;
        grounded = isGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private bool isGrounded()
    {
        float extraHeightTest = 1f;
        bool raycastHit = Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.bounds.extents.y + extraHeightTest);
        Color rayColor;
        if (raycastHit)
        {
            rayColor = Color.green;
        } else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center, Vector3.down *  (boxCollider.bounds.extents.y + extraHeightTest), rayColor);
        return raycastHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}
