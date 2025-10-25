using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timer = 0f;

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
        HUD.instance.upTimer(getTimeString(timer));
    }

    public static string getTimeString(float time)
    {
        int minute = Mathf.FloorToInt(time / 60f);
        float second = time - minute * 60;


        return string.Format("{0:00}:{1:00.00}", minute, second);
    }

    public void FinishGame(bool IsWin)
    {
        finishLevelUI.Open(IsWin);
        isPause = true;
    }
}
