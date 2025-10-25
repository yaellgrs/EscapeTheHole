using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private FinishLevelUI finishLevelUI;

    public bool isPause;


    private void Update()
    {
        if(!isPause)timer += Time.deltaTime;
        HUD.instance.upTimer(getTimeString());
    }

    public string getTimeString()
    {
        int minute = Mathf.FloorToInt(timer / 60f);
        float second = timer - minute * 60;


        return string.Format("{0:00}:{1:00.00}", minute, second);
    }

    public void FinishGame()
    {
        finishLevelUI.Open();
        isPause = true;
    }
}
