using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Image lifeBarre;
    [SerializeField] private Image lifeLerpBarre;
    public float xp = 0;
    private float xpMax = 4f;
    public int level = 1;

    public float lifeMax = 10f;
    private float life;
    private float lifeRegen = 1f;


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

        life = lifeMax;

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
        lifeBarre.fillAmount = Mathf.Lerp(lifeBarre.fillAmount, (life / lifeMax), 3 * Time.deltaTime);
        lifeLerpBarre.fillAmount = Mathf.Lerp(lifeLerpBarre.fillAmount, (life / lifeMax), 1f * Time.deltaTime);

        RegenLife();
    }

    private void RegenLife()
    {
        if(life >= lifeMax) return;
        lifeRegen -= Time.deltaTime;
        if(lifeRegen <= 0f)
        {
            lifeRegen = 1f;
            life = Mathf.Clamp(life + 1f, 0, lifeMax);
        }
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

    public void getDamage(float amount)
    {
        life -= amount;
        if (life <= 0) {
            GameManager.Instance.FinishGame();
            Destroy(gameObject);
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
        //if (agent == null) return; 

        if (isfocusPlayer && player != null && player.fruit != null)
            agent.SetDestination(player.fruit.transform.position);
        else if (focusedFruit != null)
        {
            agent.SetDestination(focusedFruit.transform.position);
        }
/*        else
            agent.ResetPath(); // pour éviter qu’il reste bloqué*/
    }

    private Fruit getFocusedFruit()
    {
        Debug.Log("fruits : " + fruits.Count);
        
        foreach(Fruit fruit in fruits.ToList())
        {
            if(fruit == null) continue;
            if (fruit.level <= level) {

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
