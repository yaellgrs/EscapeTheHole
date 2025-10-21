using UnityEngine;

public class Fruit : MonoBehaviour
{

    public float xp = 1f;
    public int level = 1;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log( gameObject.name + " : " + collision.gameObject.name +  " : " +collision.collider);
    }
}
