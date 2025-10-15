using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;
    private Vector3 move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        int solLayer = LayerMask.NameToLayer("Ground");
        int objetLayer = LayerMask.NameToLayer("Fruit");


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
            // Déplacement
            Vector3 moveDir = move.normalized;
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

            // Rotation selon le vecteur de mouvement
            Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
        }
    }
    
}

