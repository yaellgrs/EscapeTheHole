using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;

    private Button Btn_home;
    private Button Btn_restart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        document.gameObject.SetActive(false);
    }



    private void OnEnable()
    {
        var root = document.rootVisualElement;

        Btn_home = root.Q<Button>("home");

        Btn_home.clicked += HomeClicked;


    }

    public void Open()
    {
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

    private void OnDisable()
    {
        Btn_home.clicked -= HomeClicked;
    }
}
