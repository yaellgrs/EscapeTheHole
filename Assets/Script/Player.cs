using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Hole hole;

    public float speed = 5f;
    public float rotationSpeed = 2f;
    private float sprintTime = 1f;
    private float sprintSpeed = 0f;

    private Rigidbody rb;
    private Vector3 move;

    public int level = 3;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        int solLayer = LayerMask.NameToLayer("Ground");
        int objetLayer = LayerMask.NameToLayer("Fruit");

        rb.AddForce(Physics.gravity * 3, ForceMode.Acceleration);


    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = new Vector3(horizontal, 0, vertical).normalized;

        upSprint();
    }

    private void upSprint()
    {
        if(sprintTime < 1f) sprintTime += Time.deltaTime/5;
        if(sprintTime > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            sprintSpeed = 3f;
            sprintTime -= Time.deltaTime;
        }
        else
        {
            sprintSpeed = 0f;
        }
        if(sprintTime < 0f) sprintTime = 0f;

        HUD.instance.upSprint((sprintTime / 1) * 100f);
    }

    private bool isNearToHole()
    {
        if (Vector3.Distance(transform.position, hole.transform.position) < 2f)  return true;
        
        return false;
    }

    private void FixedUpdate()
    {
        if (move.magnitude > 0.01f)
        {
            // Déplacement
            Vector3 moveDir = move.normalized;


            float Speed = speed + sprintSpeed;
            if (isNearToHole()){ 
                Speed /= 3f;
            }

            rb.MovePosition(rb.position + moveDir * Speed * Time.fixedDeltaTime);

            // Rotation selon le vecteur de mouvement
            Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
        }
    }
    
}
