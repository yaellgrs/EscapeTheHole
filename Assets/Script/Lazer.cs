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
        Hole hole = other.GetComponent<Hole>();

        if (hole != null)
        {
            Debug.Log("hole before xp : " + hole.xp);
            if( hole.xp > 0 ) hole.addXp(-1);
            Debug.Log("hole after xp : " + hole.xp);


        }
        else
        {
            Debug.Log("hole not find but : " + other.gameObject.name);
        }

    }
}
