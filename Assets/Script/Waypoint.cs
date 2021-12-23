using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    GroundTile groundTile;
    BotMovement botMove;

    void Start()
    {
        botMove = FindObjectOfType<BotMovement>();
        groundTile = FindObjectOfType<GroundTile>();
        Destroy(gameObject,60);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bot")
        {
            botMove.isTurning = true;
            Destroy(gameObject);
        }
    }

}
