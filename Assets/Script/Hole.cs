using UnityEngine;
using UnityEngine.AI;

public class Hole : MonoBehaviour
{
    public Collider solCollider;
    public Player player;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null ) agent.SetDestination(player.transform.position);
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
