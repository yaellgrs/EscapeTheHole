using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = new Vector3(horizontal, 0, vertical).normalized;


    }

    private void FixedUpdate()
    {
        if (move.magnitude > 0.01f)
        {
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

            // Rotation progressive (smooth)
            Quaternion targetRot = Quaternion.LookRotation(move);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}

