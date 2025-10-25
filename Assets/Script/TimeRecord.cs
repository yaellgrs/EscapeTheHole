using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeRecord", menuName = "Scriptable Objects/TimeRecord")]
public class TimeRecord : ScriptableObject
{
    public static TimeRecord Instance;

    private void OnEnable()
    {
        Instance = this; // assignation automatique quand l'asset est chargé
    }


    // Enregistre le temps si c'est un record
    public void RecordTime(string sceneName, float time)
    {
        string key = "Record_" + sceneName;

        // Vérifie s'il existe déjà un record et si le nouveau est meilleur
        if (!PlayerPrefs.HasKey(key) || time < PlayerPrefs.GetFloat(key))
        {
            PlayerPrefs.SetFloat(key, time);
            PlayerPrefs.Save();
            Debug.Log($"Nouveau record pour {sceneName} : {time:F2} secondes");
        }
    }

    public float GetRecord(string sceneName)
    {
        string key = "Record_" + sceneName;
        return PlayerPrefs.GetFloat(key, 0f); // 0 = pas de record
    }
}
