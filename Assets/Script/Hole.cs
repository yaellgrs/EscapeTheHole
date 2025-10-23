using System.Collections.Generic;
using System.IO;
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
    public bool isfocusPlayer = true;
    public Fruit focusedFruit;
    public List<Fruit> fruits;

    [SerializeField] private Image xpBarre;
    public float xp;
    private float xpMax = 4f;
    private int level = 1;


    private float baseY;
    private void Awake()
    {
        baseY = transform.position.y;
    }

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

        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;

        calculFocusing();
        setFocusing();

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

    public void calculFocusing()
    {
        if (setFocusTimer <= 0f)
        {
            setFocusTimer = timeTrackingPlayer;

                

            if (isfocusPlayer) isfocusPlayer = false;
            else{
                isfocusPlayer = Random.Range(0, 4) != 1;

                if (level < player.level) isfocusPlayer = false;
                Debug.Log("hole level : " + level + " player level : " + player.level);
            }

            if (!isfocusPlayer)
            {
                focusedFruit = getFocusedFruit();
                if (focusedFruit == null) isfocusPlayer = true; 
            }
        }

         setFocusTimer -= Time.deltaTime;
    }

    public void setFocusing()
    {
        if(focusedFruit != null ) Debug.Log("Focused fruit: " + focusedFruit.name + " Distance: " + Vector3.Distance(transform.position, focusedFruit.transform.position));
        if (agent == null){ Debug.Log("pas d'agent"); return; }

        if (isfocusPlayer && player != null && player.fruit != null)
            agent.SetDestination(player.fruit.transform.position);
        else if (focusedFruit != null)
        {
            agent.SetDestination(focusedFruit.transform.position);
            NavMeshPath path = new NavMeshPath();
            bool canReach = agent.CalculatePath(focusedFruit.transform.position, path);
            Debug.Log("Can reach: " + canReach + " Corners: " + path.corners.Length);
            Debug.Log("Agent stopped: " + agent.isStopped);
            Debug.Log("Agent velocity: " + agent.velocity);
            Debug.Log("Agent remainingDistance: " + agent.remainingDistance);
        }
        else
            agent.ResetPath(); // pour éviter qu’il reste bloqué
    }

    private Fruit getFocusedFruit()
    {
        foreach(Fruit fruit in fruits.ToList())
        {
            if(fruit == null) continue;
            if (fruit.level <= level) {

                return fruit; 
            }
        }
        Debug.Log("aucun fruit trouvé");
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
