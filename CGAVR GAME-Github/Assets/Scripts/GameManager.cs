using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    public List<GameObject> target;
    public List<GameObject> powerUp;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject inGameScreen;
    private int health;
    private float spawnRate;
    public int powerUpSpawnTime;
    public bool gameOver;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public ParticleSystem healthParticle1;
    public ParticleSystem healthParticle2;
    public ParticleSystem healthParticle3;
    public int gameDifficulty;
    private MainManager mainManager;
    public TextMeshProUGUI currentPlayerNameText;
    public TextMeshProUGUI bestPlayerName_ScoreText;
    public string currentPlayerName;
    public int currentPlayerScore;
    public string bestPlayerName;
    public int bestPlayerScore = 0;
    // Start is called before the first frame update
    void Start()
{
    // Ensure that mainManager is not null before using it
    mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    if (mainManager == null)
    {
        Debug.LogError("MainManager is not found!");
        return;  // Stop further execution to avoid null references
    }

    mainManager.LoadBestPlayerData1();
    bestPlayerName = mainManager.bestPlayerName;  // Assign best player name from MainManager
    bestPlayerScore = mainManager.bestPlayerScore;  // Assign best player score from MainManager

    // Ensure bestPlayerName_ScoreText is not null before trying to set it
    if (bestPlayerName_ScoreText != null)
    {
        bestPlayerName_ScoreText.SetText(bestPlayerName + " :- " + bestPlayerScore);
    }
    else
    {
        Debug.LogError("bestPlayerName_ScoreText is not assigned in the Inspector!");
    }

    // Ensure currentPlayerNameText is not null before trying to set it
    if (currentPlayerNameText != null)
    {
        currentPlayerNameText.SetText(currentPlayerName);
    }
    else
    {
        Debug.LogError("currentPlayerNameText is not assigned in the Inspector!");
    }
}


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget()
    {
        while (gameOver == false)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = UnityEngine.Random.Range(0, target.Count);
            Instantiate(target[index]);
            if (gameDifficulty == 1)
            {
                int descision = UnityEngine.Random.Range(0, 10);
                if (descision == 4)
                {
                    int index1 = UnityEngine.Random.Range(0, target.Count);
                    Instantiate(target[index1]);
                }
            }
            if (gameDifficulty == 2)
            {
                int descision = UnityEngine.Random.Range(0, 4);
                if (descision == 3)
                {
                    int index2 = UnityEngine.Random.Range(0, target.Count);
                    Instantiate(target[index2]);
                }
            }
            if (gameDifficulty == 3)
            {
                int descision = UnityEngine.Random.Range(0, 2);
                if (descision == 0)
                {
                    int index3 = UnityEngine.Random.Range(0, target.Count);
                    Instantiate(target[index3]);
                }
            }
        }
    }
    IEnumerator SpawnPowerUp()
    {
        while (gameOver == false)
        {
            yield return new WaitForSeconds(powerUpSpawnTime);
            int decisionSpawnPowerUp = UnityEngine.Random.Range(0, 11);
            if ((decisionSpawnPowerUp == 10 || decisionSpawnPowerUp == 7) && health < 3)
            {
                Debug.Log("PowerUp Spawned");
                int index1 = UnityEngine.Random.Range(0, powerUp.Count);
                Instantiate(powerUp[index1]);
            }
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        currentPlayerScore += scoreToAdd;
        scoreText.text = "Score : " + currentPlayerScore;

    }
    public void GameOver()
{
    gameOver = true;
    
    // Ensure gameOverScreen is not null before activating it
    if (gameOverScreen != null)
    {
        gameOverScreen.SetActive(true);
    }
    else
    {
        Debug.LogError("gameOverScreen is not assigned in the Inspector!");
    }

    // Check and update best player score
    if (currentPlayerScore > bestPlayerScore)
    {
        bestPlayerScore = currentPlayerScore;
        bestPlayerName = currentPlayerName;
        mainManager.SaveBestPlayerData1();  // Save the new best player data
    }

    // Ensure bestPlayerName_ScoreText is not null before updating it
    if (bestPlayerName_ScoreText != null)
    {
        bestPlayerName_ScoreText.SetText(bestPlayerName + " :- " + bestPlayerScore);
    }
}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        powerUpSpawnTime = difficulty + 2;
        inGameScreen.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        currentPlayerNameText.SetText(currentPlayerName);
        bestPlayerName_ScoreText.SetText(bestPlayerName + " :- " + bestPlayerScore);
        gameOver = false;
        health = 3;
        currentPlayerScore = 0;
        if (difficulty == 1)
        {
            spawnRate = 1;
        }
        else if (difficulty == 2)
        {
            spawnRate = 0.85f;
        }
        else if (difficulty == 3)
        {
            spawnRate = 0.7f;
        }
        UpdateIncreaseHealth();
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnPowerUp());
    }
    public void ReduceHealth()
    {
        health--;
        UpdateDecreaseHealth();
        if (health == 0)
        {
            GameOver();
        }


    }
    public void IncreaseHealth()
    {
        if (health < 3)
        {
            health++;
            Debug.Log("Health Added");
            UpdateIncreaseHealth();
        }
    }
    private void UpdateDecreaseHealth()
    {
        if (health == 3)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health3.gameObject.SetActive(true);
        }
        else if (health == 2)
        {
            health3.gameObject.SetActive(false);
            healthParticle3.Play();
        }
        else if (health == 1)
        {
            health2.gameObject.SetActive(false);
            healthParticle2.Play();
        }
        else if (health == 0)
        {
            health1.gameObject.SetActive(false);
            healthParticle1.Play();
        }
    }
    private void UpdateIncreaseHealth()
    {
        if (health == 3)
        {
            health3.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health1.gameObject.SetActive(true);
        }
        else if (health == 2)
        {
            health2.gameObject.SetActive(true);
            health1.gameObject.SetActive(true);
        }

    }
    public void AddPowerUpEffect(string powerUpCategory)
    {
        Debug.Log(powerUpCategory);
        if (powerUpCategory == "Health Kit")
        {
            IncreaseHealth();
        }
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void ReduceScoreOnMissClick(int reduceScore)
    {
        UpdateScore(reduceScore);
        Debug.Log("Score is reduced by " + reduceScore + " points on Miss-Click");
    }
    public void UpdateBestPlayerData()
    {
        mainManager.SaveBestPlayerData1();
    }
}
