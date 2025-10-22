using UnityEngine;

public class HoleCleaner : MonoBehaviour
{

    public Hole hole;
    private void OnTriggerEnter(Collider other)
    {
        hole.setFocusTimer = 0f;
        Fruit fruit = other.gameObject.GetComponent<Fruit>();
        if (fruit != null)
        {
            hole.addXp(fruit.xp);

        }


        Destroy(other.gameObject);
    }


}
