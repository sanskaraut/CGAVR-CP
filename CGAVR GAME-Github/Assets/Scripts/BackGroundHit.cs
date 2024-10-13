using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundHit : MonoBehaviour
{
   private Rigidbody2D targetRb;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void OnMouseDown()
    {
        if (!gameManager.gameOver)
        {
            // Reduce score when the background is clicked
            gameManager.ReduceScoreOnMissClick(pointValue);
        }
    }
}
