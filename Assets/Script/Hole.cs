using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {

            int solLayer = LayerMask.NameToLayer("Ground");
            int objetLayer = LayerMask.NameToLayer("Fruit");

            Physics.IgnoreLayerCollision(solLayer, objetLayer, true);
        }
    }
}
