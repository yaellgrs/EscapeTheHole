using UnityEngine;
using UnityEngine.UIElements;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private SelectLevelUI selectLevelUI;

    private Label Lbl_title;

    private Button Btn_Play;
    private Button Btn_Exit;


    private void OnEnable()
    {
        var root = document.rootVisualElement;

        Lbl_title = root.Q<Label>("title");

        Btn_Play = root.Q<Button>("play");
        Btn_Exit = root.Q<Button>("exit");

        setAnim();

        Btn_Play.clicked += Play;
        Btn_Exit.clicked += Exit;
    }

    private void setAnim()
    {
        Lbl_title.AddToClassList("LblTitleTop");
        Lbl_title.schedule.Execute(() =>
        {
            Lbl_title.RemoveFromClassList("LblTitleTop");
        }).StartingIn(750);

        Btn_Play.AddToClassList("BtnPlayBottom");

        Btn_Play.schedule.Execute(() =>
        {
            Btn_Play.RemoveFromClassList("BtnPlayBottom");
        }).StartingIn(1000);
        Btn_Exit.AddToClassList("BtnExitBottom");
        Btn_Exit.schedule.Execute(() =>
        {
            Btn_Exit.RemoveFromClassList("BtnExitBottom");
        }).StartingIn(1000);
    }

    private void Play()
    {
        Close();
    }

    public void Open()
    {
        document.gameObject.SetActive(true);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Close()
    {
        selectLevelUI.Open();
        document.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Btn_Play.clicked -= Play;
        Btn_Exit.clicked -= Exit;
    }
}
