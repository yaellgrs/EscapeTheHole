using UnityEngine;

public class HoleCleaner : MonoBehaviour
{

    public Hole hole;
    private void OnTriggerEnter(Collider other)
    {
        hole.setFocusTimer = 0f;
        Destroy(other.gameObject);
    }


}
