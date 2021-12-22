using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] ParticleSystem explosionpt;

    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMovement.ObstacleHit();
            playerMovement.Die();
            Destroy(gameObject);
            Instantiate(explosionpt,transform.position,explosionpt.transform.rotation);
        }

    }


    void Update()
    {
        
    }
}
