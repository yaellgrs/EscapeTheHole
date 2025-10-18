using UnityEngine;

public class HoleCleaner : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }


}
