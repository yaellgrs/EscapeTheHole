using UnityEngine;
using UnityEngine.AI;

public class Hole : MonoBehaviour
{
    public Collider solCollider;
    public Player player;
    private NavMeshAgent agent;

    public float setFocusTimer = 3f;
    private bool isfocusPlayer = true;
    private GameObject focusedFruit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {   
        calculFocusing();
        setFocusing();
    }

    private void calculFocusing()
    {
        if (setFocusTimer <= 0f)
        {
            setFocusTimer = 3f;
            isfocusPlayer = Random.Range(0, 3) == 1;
            if (!isfocusPlayer)
            {
                focusedFruit = GameObject.FindGameObjectsWithTag("Fruit")[1];
            }
        }

        if (isfocusPlayer) setFocusTimer -= Time.deltaTime;
    }

    private void setFocusing()
    {
        if (isfocusPlayer && player != null) agent.SetDestination(player.transform.position);
        else if (focusedFruit != null) agent.SetDestination(focusedFruit.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Collider fruitCollider = other.GetComponent<Collider>();

            Physics.IgnoreCollision(fruitCollider, solCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Collider fruitCollider = other.GetComponent<Collider>();

            Physics.IgnoreCollision(fruitCollider, solCollider, false);
        }
    }
}
