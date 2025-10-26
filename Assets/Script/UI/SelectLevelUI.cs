using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private LobbyUI lobbyUI;

    private Label Lbl_title;

    private VisualElement VE_levels;
    private VisualElement VE_reset;

    private Button Btn_level1;
    private Button Btn_level2;
    private Button Btn_back;
    private Button Btn_reset;

    private Label Lbl_level1;
    private Label Lbl_level2;




    private void Start()
    {
        Close();
    }


    private void OnEnable()
    {
        var root = document.rootVisualElement;

        Lbl_title = root.Q<Label>("title");
        Lbl_level1 = root.Q<Label>("level1time");
        Lbl_level2 = root.Q<Label>("level2time");
        VE_levels = root.Q<VisualElement>("levels");
        VE_reset = root.Q<VisualElement>("resetVE");

        Btn_level1 = root.Q<Button>("level1");
        Btn_level2 = root.Q<Button>("level2");

        Btn_reset = root.Q<Button>("reset");
        Btn_back = root.Q<Button>("back");

        setAnim();
        Btn_level1.clicked += ()=> { SceneManager.LoadScene("level1"); } ;
        Btn_level2.clicked += ()=> { SceneManager.LoadScene("level2"); } ;
        Btn_back.clicked += Back;
        Btn_reset.clicked += resetClicked;

        UpdateLabel(Lbl_level1, "level1");
        UpdateLabel(Lbl_level2, "level2");
    }
    private void UpdateLabel(Label label, string level)
    {
        if (TimeRecord.Instance != null)
        {

            var record = TimeRecord.Instance.GetRecord(level);

            // Vérifie si le record est valide
            if (record <= 0 )
            {
                label.text = "--:--";
            }
            else
            {
                label.text = GameManager.getTimeString(record);
            }
        }
        else
        {
            label.text = "--:--";
        }
    }

    private void setAnim()
    {
        Lbl_title.AddToClassList("LblTitleTop");
        Lbl_title.schedule.Execute(() =>
        {
            Lbl_title.RemoveFromClassList("LblTitleTop");
        }).StartingIn(1);

        Btn_back.AddToClassList("BtnExitBottom");
        Btn_back.schedule.Execute(() =>
        {
            Btn_back.RemoveFromClassList("BtnExitBottom");
        }).StartingIn(250);

        VE_reset.AddToClassList("resetVEBottom");
        VE_reset.schedule.Execute(() =>
        {
            VE_reset.RemoveFromClassList("resetVEBottom");
        }).StartingIn(250);


        VE_levels.AddToClassList("VELevelLeft");
        VE_levels.schedule.Execute(() =>
        {
            VE_levels.RemoveFromClassList("VELevelLeft");
        }).StartingIn(500);
    }

    private void resetClicked()
    {

        TimeRecord.Instance.RecordTime("level1", 0);
        TimeRecord.Instance.RecordTime("level2", 0);
        UpdateLabel(Lbl_level1, "level1");
        UpdateLabel(Lbl_level2, "level2");
    }

    

    private void Back()
    {
        lobbyUI.Open();
        Close();

    }

    public void Open()
    {
        document.gameObject.SetActive(true);
    }
    private void Close()
    {
        if (document != null && document.gameObject != null) document.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Btn_level1.clicked -= () => { SceneManager.LoadScene("level1"); };
        Btn_level2.clicked -= () => { SceneManager.LoadScene("level2"); };
        Btn_back.clicked -= Back;
        Btn_reset.clicked -= resetClicked;
    }
}
