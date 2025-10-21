using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
/*        rb = GetComponent<Rigidbody>();

        rb.AddForce(GetComponent<Transform>().forward * 500f);*/
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            other.GetComponent<Hole>().addXp(-1);
            Destroy(gameObject);
        }
    }
}
