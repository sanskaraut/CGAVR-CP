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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartGame;
    private int score = 0;
    public int health = 3;
    public float spawnRate = 3;
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
        scoreText.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        gameOver = false;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
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
    }
    private void UpdateHealth()
    {
        if(health == 2)
        {
            health3.gameObject.SetActive(false);
            healthParticle3.Play();
        }
        if(health == 1)
        {
            health2.gameObject.SetActive(false);
            healthParticle2.Play();
        }
        if(health == 0)
        {
            health1.gameObject.SetActive(false);
            healthParticle1.Play();
        }
        
    }
}
