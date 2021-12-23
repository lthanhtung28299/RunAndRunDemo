using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    BotMovement botMv;
    PlayerMovement playerMv;
    GameManager gameManager;
    void Start()
    {
        transform.position = new Vector3(Random.Range(100,700),0,0);
        gameManager = FindObjectOfType<GameManager>();
        botMv = FindObjectOfType<BotMovement>();
        playerMv = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMv.PlayerWin();
            botMv.BotLose();
            gameManager.GameOver();
        }
        else if(other.gameObject.tag == "Bot")
        {
            playerMv.PlayerLose();
            botMv.BotWin();
            gameManager.GameOver();
        }

    }
}
