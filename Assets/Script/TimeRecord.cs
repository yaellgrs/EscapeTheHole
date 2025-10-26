using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeRecord", menuName = "Scriptable Objects/TimeRecord")]
public class TimeRecord : ScriptableObject
{
    private static TimeRecord _instance;
    public static TimeRecord Instance
    {
        get
        {
            if (_instance == null)
            {
                // Essaie de charger depuis Resources s'il existe un asset
                _instance = Resources.Load<TimeRecord>("TimeRecord");

                // Si rien trouvé, on en crée un (pour éviter un crash)
                if (_instance == null)
                {
                    _instance = ScriptableObject.CreateInstance<TimeRecord>();
                }
            }
            return _instance;
        }
    }

    private void OnEnable()
    {
    }


    // Enregistre le temps si c'est un record
    public void RecordTime(string sceneName, float time)
    {
        string key = "Record_" + sceneName;

        // Vérifie s'il existe déjà un record et si le nouveau est meilleur
        if (!PlayerPrefs.HasKey(key) || time < PlayerPrefs.GetFloat(key) || PlayerPrefs.GetFloat(key) <= 0f)
        {
            PlayerPrefs.SetFloat(key, time);
            PlayerPrefs.Save();
        }
    }

    public float GetRecord(string sceneName)
    {
        string key = "Record_" + sceneName;
        return PlayerPrefs.GetFloat(key, 0f); // 0 = pas de record
    }
}
