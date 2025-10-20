using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class Hole : MonoBehaviour
{
    public Collider solCollider;
    public Player player;
    private NavMeshAgent agent;

    public float setFocusTimer = 5f;
    private float timeTrackingPlayer = 5f;
    private bool isfocusPlayer = true;
    private Fruit focusedFruit;
    public List<Fruit> fruits;

    [SerializeField] private Image xpBarre;
    public float xp;
    private float xpMax = 4f;
    private int level = 1;


    private float baseY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        xpBarre.fillAmount = xp / xpMax;

        fruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None).ToList();
        Debug.Log(fruits.Count);

    }

    void Update()
    {   
        calculFocusing();
        setFocusing();

        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;


        //xpBarre.fillAmount = 0.1f + (xp / xpMax) * (0.9f - 0.1f);
        xpBarre.fillAmount = Mathf.Lerp(xpBarre.fillAmount, 0.1f + (xp / xpMax) * (0.9f - 0.1f), 3* Time.deltaTime);
    }

    public void addXp(float amount)
    {
        xp = Mathf.Clamp(xp + amount, 0, xpMax);
        if(xp >= xpMax)
        {
            xp = 0;
            xpMax *= 2;
            transform.localScale *= 1.5f;
            level++;
        }
    }

    private void calculFocusing()
    {
        if (setFocusTimer <= 0f)
        {
            setFocusTimer = timeTrackingPlayer;

                

            if (isfocusPlayer) isfocusPlayer = false;
            else{
                isfocusPlayer = Random.Range(0, 4) != 1;

                if (level < player.level) isfocusPlayer = false;
            }

            if (!isfocusPlayer)
            {
                focusedFruit = getFocusedFruit();
                if (focusedFruit == null) isfocusPlayer = true; 
            }
        }

         setFocusTimer -= Time.deltaTime;
    }

    private void setFocusing()
    {
        if (isfocusPlayer && player != null) agent.SetDestination(player.fruit.transform.position);
        else if (focusedFruit != null) agent.SetDestination(focusedFruit.transform.position);
    }

    private Fruit getFocusedFruit()
    {
        foreach(Fruit fruit in fruits)
        {
            if(fruit == null) continue;
            if (fruit.level <= level) {
                fruits.Remove(fruit);
                return fruit; 
            }
        }
        return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Collider fruitCollider = other.GetComponent<Collider>();

            Physics.IgnoreCollision(fruitCollider, solCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            Collider fruitCollider = other.GetComponent<Collider>();

            Physics.IgnoreCollision(fruitCollider, solCollider, false);
        }
    }
}
