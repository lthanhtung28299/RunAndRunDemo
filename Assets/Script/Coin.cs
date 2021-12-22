using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int scorePerCoint = 100;
    [SerializeField] float yAngle = 1f;
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    [SerializeField] ParticleSystem coinPt;
    GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            gameManager.AddScore(scorePerCoint);
            Destroy(gameObject);
            Instantiate(coinPt,transform.position,coinPt.transform.rotation);
        }

    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        startingPosition = transform.position;
    }


    void Update()
    {
        if(period <= Mathf.Epsilon) {return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        
        transform.Rotate(0,yAngle,0);
    }

}
