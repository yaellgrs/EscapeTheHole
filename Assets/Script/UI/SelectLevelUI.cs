using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private LobbyUI lobbyUI;

    private Label Lbl_title;

    private VisualElement VE_levels;

    private Button Btn_level1;
    private Button Btn_back;



    private void Start()
    {
        Close();
        Debug.Log("close");
    }


    private void OnEnable()
    {
        Debug.Log("Enable SelectLevelUI");
        var root = document.rootVisualElement;

        Lbl_title = root.Q<Label>("title");
        VE_levels = root.Q<VisualElement>("levels");

        Btn_level1 = root.Q<Button>("level1");

        Btn_back = root.Q<Button>("back");

        setAnim();
        Btn_level1.clicked += ()=> { SceneManager.LoadScene("level1"); } ;
        Btn_back.clicked += Back;
    }

    private void setAnim()
    {
        Lbl_title.AddToClassList("LblTitleTop");
        Lbl_title.schedule.Execute(() =>
        {
            Lbl_title.RemoveFromClassList("LblTitleTop");
        }).StartingIn(750);

        Btn_back.AddToClassList("BtnExitBottom");
        Btn_back.schedule.Execute(() =>
        {
            Btn_back.RemoveFromClassList("BtnExitBottom");
        }).StartingIn(1000);


        VE_levels.AddToClassList("VELevelLeft");
        VE_levels.schedule.Execute(() =>
        {
            VE_levels.RemoveFromClassList("VELevelLeft");
        }).StartingIn(1250);
    }

    

    private void Back()
    {
        lobbyUI.Open();
        Close();

    }

    public void Open()
    {
        Debug.Log("Open SelectLevelUI");
        document.gameObject.SetActive(true);
    }
    private void Close()
    {
        if (document != null && document.gameObject != null) document.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Btn_level1.clicked -= () => { SceneManager.LoadScene("level1"); };
        Btn_back.clicked -= Back;
    }
}
