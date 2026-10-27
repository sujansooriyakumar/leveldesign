using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    // file path
    // party data

    [ContextMenu("Save")]
    public void SaveData()
    {
        string filePath = "Assets/Resources/save.json";
        string json = JsonUtility.ToJson(PartyManager.Instance.party, true);
        File.WriteAllText(filePath, json);
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        string filePath = "Assets/Resources/save.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PartyManager.Instance.party = JsonUtility.FromJson<Party>(json);
        }
    }
}
