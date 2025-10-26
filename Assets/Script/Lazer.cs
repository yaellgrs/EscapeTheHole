using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody rb;
    public int damage = 1;
    private void Start()
    {
/*        rb = GetComponent<Rigidbody>();

        rb.AddForce(GetComponent<Transform>().forward * 500f);*/
    }


    private void OnTriggerEnter(Collider other)
    {
        Hole hole = other.GetComponent<Hole>();

        if (hole != null)
        {
            hole.getDamage(damage);
        }

    }
}
