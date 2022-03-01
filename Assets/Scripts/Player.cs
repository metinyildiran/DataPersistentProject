using System;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string playerName;
    [HideInInspector] public int bestScore;

    public static Player SharedInstance;

    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    private class SaveData
    {
        public string playerName;
        public int bestScore;
    }

    public void SavePlayerAndBestScore(int newScore)
    {
        var saveData = new SaveData();
        saveData.playerName = playerName;
        saveData.bestScore = newScore;

        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerAndBestScore()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            playerName = saveData.playerName;
            bestScore = saveData.bestScore;
        }
    }

    public string GetBestScoreText()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            return "Best Score: " + saveData.playerName + ": " + saveData.bestScore;
        }

        return "Best Score: John Doe: 0";
    }
}