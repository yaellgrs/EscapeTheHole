using UnityEngine;

public class Fruit : MonoBehaviour
{

    public float xp = 1f;
    public int level = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            transform.position += Vector3.up * 0.5f;    
        }
    }
}
