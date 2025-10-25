using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    private Label Lbl_timer;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    public UIDocument document;

    private VisualElement VE_filledSprint;


    private void OnEnable()
    {
        var root = document.rootVisualElement;
        VE_filledSprint = root.Q<VisualElement>("filledSprint");
        Lbl_timer = root.Q<Label>("timer");


    }

    public void upTimer(string time)
    {


        Lbl_timer.text = time;

    }

    public void upSprint(float percent)
    {
        VE_filledSprint.style.width = Length.Percent(percent);
    }

    private void OnDisable()
    {
        
    }
}
