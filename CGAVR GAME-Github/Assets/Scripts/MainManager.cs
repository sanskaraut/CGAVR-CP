using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string currentPlayerName;
    public int currentPlayerScore;
    public string bestPlayerName;
    public int bestPlayerScore;
    private GameManager gameManager;
    // Start is called before the first frame update
    // void Awake()
    // {
    //     if (Instance != null)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     else
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     // SaveBestPlayerName();
    //     // SaveBestPlayerScore();
    //     // LoadBestPlayerName();
    //     // LoadBestPlayerScore();
    // }
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    public void ReadPlayerNameInput(string input)
    {
        currentPlayerName = input;
        Debug.Log(currentPlayerName);
        gameManager.currentPlayerName = currentPlayerName;
        // SaveBestPlayerName();
    }
    public void SaveBestPlayerData1()
    {
        SaveBestPlayerData data = new SaveBestPlayerData();
        data.bestPlayerNameInJson = gameManager.currentPlayerName;
        data.bestPlayerScoreInJson = gameManager.currentPlayerScore;
        Debug.Log("The best Player is now" + data.bestPlayerNameInJson + " :- " + data.bestPlayerScoreInJson);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadBestPlayerData1()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveBestPlayerData data = JsonUtility.FromJson<SaveBestPlayerData>(json);

            bestPlayerName = data.bestPlayerNameInJson;
            bestPlayerScore = data.bestPlayerScoreInJson;

            gameManager.bestPlayerName = bestPlayerName;
            gameManager.bestPlayerScore = bestPlayerScore;
        }
    }
    [System.Serializable]
    public class SaveBestPlayerData
    {
        public string bestPlayerNameInJson;
        public int bestPlayerScoreInJson;
    }
}
