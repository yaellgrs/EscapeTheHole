using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private FinishLevelUI finishLevelUI;

    public bool isPause;


    public void FinishGame()
    {
        finishLevelUI.Open();
        isPause = true;
    }
}
