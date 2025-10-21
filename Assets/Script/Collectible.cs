using UnityEngine;

public class Collectible : MonoBehaviour
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Player player = other.GetComponentInParent<Player>();
            if(player != null) player.addXp(1);
            Destroy(gameObject);
        }


    }
}
