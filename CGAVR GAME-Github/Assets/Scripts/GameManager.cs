using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    public List<GameObject> target;
    public List<GameObject> powerUp;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartGame;
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
        }
    }
    IEnumerator SpawnPowerUp()
    {
        while (gameOver == false)
        {
            yield return new WaitForSeconds(powerUpSpawnTime);
            int decisionSpawnPowerUp = UnityEngine.Random.Range(0, 11);
            if (decisionSpawnPowerUp == 10)
            {
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
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnPowerUp());
        UpdateScore(0);
        UpdateHealth();
    }
    public void ReduceHealth()
    {
        health--;
        UpdateHealth();
        if (health == 0)
        {
            GameOver();
        }


    }
    public void IncreaseHealth()
    {
        health++;
        UpdateHealth();
        Debug.Log("Health Added");
    }
    private void UpdateHealth()
    {
        if (health == 3)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health3.gameObject.SetActive(true);
        }
        else if (health == 2)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(true);
            health3.gameObject.SetActive(false);
            healthParticle3.Play();
        }
        else if (health == 1)
        {
            health1.gameObject.SetActive(true);
            health2.gameObject.SetActive(false);
            health3.gameObject.SetActive(false);
            healthParticle2.Play();
        }
        else if (health == 0)
        {
            health1.gameObject.SetActive(false);
            health1.gameObject.SetActive(false);
            health1.gameObject.SetActive(false);
            healthParticle1.Play();
        }

    }
    public void AddPowerUpEffect(string powerUpCode)
    {
        Debug.Log(powerUpCode);
        if (powerUpCode == "Health Kit")
        {
            IncreaseHealth();
        }
    }
}
