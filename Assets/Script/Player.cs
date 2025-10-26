using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("-- Hole --")]
    public Hole hole;

    [Header("-- Camera --")]
    public CinemachineFollow cameraFollow;

    [Header("-- movement --")]
    public float speed = 5f;
    public float rotationSpeed = 2f;
    private float sprintTime = 1f;
    private float sprintSpeed = 0f;
    private Vector3 move;

    [Header("-- level --")]
    public int level = 1;
    public int xp = 0;
    private int xpMax = 4;


    [Header("-- fruits --")]
    public GameObject fruit;

    public GameObject applePrefab;
    public GameObject bananaPrefab;
    public GameObject orangePrefab;
    public GameObject waterMelonPrefab;

    [Header("-- lazer --")]
    public Lazer lazerPrefab;

    [Header("-- UI --")]
    [SerializeField] private Image xpBarre;


    //other
    private Rigidbody rb;




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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Lazer lazer = Instantiate(lazerPrefab, fruit.transform.position, fruit.transform.rotation);
            lazer.damage = level;
            lazer.GetComponent<Rigidbody>().AddForce(fruit.transform.right * -900f);
        }

        xpBarre.fillAmount = Mathf.Lerp(xpBarre.fillAmount, 0.1f + (xp / (float)xpMax) * (0.9f - 0.1f), 3 * Time.deltaTime);
        if( fruit != null ) xpBarre.transform.position = fruit.transform.position;
    }

    public void addXp(int amount)
    {
        xp = Mathf.Clamp(xp + amount, 0, xpMax);
        if (xp >= xpMax)
        {
            xp = 0;
            xpMax *= 2;
            level++;
            setNextMesh();
            if(hole.isfocusPlayer)hole.setFocusTimer = 0f;
        }
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


    private void setNextMesh()
    {
        GameObject fruitPrefab = null;

        switch (level)
        {
            case 2:
                fruitPrefab = applePrefab;
                break;
            case 3:
                fruitPrefab = bananaPrefab;
                break;
            case 4:
                fruitPrefab = orangePrefab;
                break;
            case 5:
                fruitPrefab = waterMelonPrefab;
                break;
            default:
                return;
        }
        if (fruit != null) Destroy(fruit);

        fruit = Instantiate(fruitPrefab, transform);
        fruit.transform.localPosition = Vector3.zero;
        fruit.transform.localRotation = Quaternion.identity;

        //if(level ==3 ) 
        fruit.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

        cameraFollow.FollowOffset += Vector3.up;

        transform.position += Vector3.up;

        xpBarre.transform.localScale *= 1.5f;

    }


    private bool isNearToHole()
    {
        if (hole == null) return false;
        if (Vector3.Distance(transform.position, hole.transform.position) < 2f)  return true;
        
        return false;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.isPause) return;

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
