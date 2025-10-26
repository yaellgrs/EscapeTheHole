
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    private Button Btn_home;
    private Button Btn_restart;

    private Label Lbl_timer;

    private bool isWin = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        document.gameObject.SetActive(false);
    }



    private void OnEnable()
    {
        var root = document.rootVisualElement;

        Btn_home = root.Q<Button>("home");
        Btn_restart = root.Q<Button>("restart");
        Lbl_timer = root.Q<Label>("time");


        Btn_home.clicked += HomeClicked;
        Btn_restart.clicked += RestartClicked;

        if (isWin)
        {
            if (GameManager.Instance != null)
            {
                Lbl_timer.text = GameManager.getTimeString(GameManager.Instance.timer);
                
                TimeRecord.Instance.RecordTime(SceneManager.GetActiveScene().name, GameManager.Instance.timer);
            }
        }
        else Lbl_timer.text = "game over";



    }

    public void Open(bool IsWin)
    {
        isWin = IsWin;
        document.gameObject.SetActive(true);


    }

    private void HomeClicked()
    {
        SceneManager.sceneLoaded += OnLobbyLoaded;
        SceneManager.LoadScene("lobby");

    }

    private void OnLobbyLoaded(Scene scene, LoadSceneMode mode)
    {
        LobbyUI.Instance.openSelectLevel = true;
        SceneManager.sceneLoaded -= OnLobbyLoaded; // désabonne pour éviter les doublons

    }

    private void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        Btn_home.clicked -= HomeClicked;
        Btn_restart.clicked -= RestartClicked;
    }
}
