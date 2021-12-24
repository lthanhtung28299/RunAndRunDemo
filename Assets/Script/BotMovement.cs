using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [Header("BotStats")]
    [SerializeField] float botSpeed = 1f;
    [SerializeField] float botTurnSpeed = 0f;
    [SerializeField] float baseTurnSpeed = 0f;
    [SerializeField] float turnSpeed;
    
    [Header("BotParticle")]
    [SerializeField] ParticleSystem winPt;
    [SerializeField] ParticleSystem losePt;
    Rigidbody botRd;
    Animator anim;
    BoxCollider hitObstacle;
    Waypoint wayPoint;
    PlayerMovement playerMv;
    public bool isGamePlaying;
    public bool isTurning = false;

    void Start()
    {
        isGamePlaying = true;
        wayPoint = FindObjectOfType<Waypoint>();
        anim = GetComponent<Animator>();
        hitObstacle = GetComponent<BoxCollider>();
        botRd = GetComponent<Rigidbody>();
    }

    void BotRunning()
    {
        if(isTurning)
        {
            
            BotTurnSpeed();
        }
        else
        {
            transform.Translate(botTurnSpeed * Time.deltaTime,0,botSpeed * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }

    }

    public void BotTurnSpeed()
    {
        botTurnSpeed = turnSpeed;
        transform.Translate(turnSpeed * Time.deltaTime,0,botSpeed * Time.deltaTime);
        StartCoroutine(ReturnForwardRun());
    }

    public IEnumerator ReturnForwardRun()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        botTurnSpeed = baseTurnSpeed;
        turnSpeed = Random.Range(-5,5);
        isTurning = false;
    }


    void Update()
    {
        if(!isGamePlaying) return;
        
        BotRunning();
    }

    public void BotWin()
    {
        winPt.Play();
        isGamePlaying = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isHappy",true);
    }

    public void BotLose()
    {
        losePt.Play();
        isGamePlaying = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isScared", true);
    }
}
