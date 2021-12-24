using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("ScoreToAdd")]
    [SerializeField] int scorePerCoin = 100;
    [Header("TransformPickUp")]
    [SerializeField] float yAngle = 1f;
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    [Header("PickUpParticle")]
    [SerializeField] ParticleSystem powerUpPt;
    [SerializeField] ParticleSystem freezePt;
    [SerializeField] ParticleSystem coinPt;

    [Header("PickUpType")]
    [SerializeField] bool isTrap;
    [SerializeField] bool isSpeedUp;
    [SerializeField] bool isCoin;
    GameManager gameManager;
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
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


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player") return;
        if(isTrap)
        {
            playerMovement.Freeze();
            Instantiate(freezePt, transform.position, freezePt.transform.rotation);
            Destroy(gameObject);
        }
        else if(isSpeedUp)
        {
            playerMovement.PowerUpActive();
            Instantiate(powerUpPt, transform.position, powerUpPt.transform.rotation);
            Destroy(gameObject);
        }
        else if(isCoin)
        {
            gameManager.AddScore(scorePerCoin);
            playerMovement.PlayCoinSoundFX();
            Destroy(gameObject);
            Instantiate(coinPt,transform.position,coinPt.transform.rotation);
        }
        
    }
}
