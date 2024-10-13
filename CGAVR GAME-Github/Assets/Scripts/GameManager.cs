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
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartGame;
    public Button exitGame;
    private int score = 0;
    private int health = 3;
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
    // Start is called before the first frame update
    void Start()
    {

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
        score += scoreToAdd;
        scoreText.text = "Score : " + score;

    }
    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true);
        exitGame.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        powerUpSpawnTime = difficulty + 2;
        scoreText.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        gameOver = false;
        score = 0;
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
}
