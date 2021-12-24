using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowStorm : MonoBehaviour
{
    [SerializeField] float stormSpeed = 5f;
    BotMovement botMv;
    PlayerMovement playerMv;
    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        botMv = FindObjectOfType<BotMovement>();
        playerMv = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        transform.Translate(new Vector3(stormSpeed * Time.deltaTime,0,0));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMv.PlayerLose();
            botMv.BotWin();
            gameManager.GameOver();
        }
    }
}
