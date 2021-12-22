using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] float yAngle = 1f;
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    [SerializeField] ParticleSystem powerUpPt;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
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
        playerMovement.PowerUpActive();
        Destroy(gameObject);
        Instantiate(powerUpPt, transform.position, powerUpPt.transform.rotation);
    }
}
