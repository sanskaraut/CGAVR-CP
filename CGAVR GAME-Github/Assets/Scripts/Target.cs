using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private int minForce = 12;
    private int maxForce = 17;
    private int maxTorque = 6;
    private int xRange = 4;
    private float yRange = -2.5f;
    public bool reduceHealth;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPos();
        audioSource = GetComponent<AudioSource>();
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), yRange, 0);
    }

    void OnMouseDown()
    {
        if (gameManager.gameOver == false)
        {

            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (reduceHealth)
            {
                gameManager.ReduceHealth();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            Destroy(gameObject);
        }
    }
}

