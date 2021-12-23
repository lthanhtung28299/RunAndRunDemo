using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Material[] playerskins;
    Renderer playerRender;
    [SerializeField] float baseSpeed = 7f;
    [SerializeField] float boostSpeed = 15f;
    float speed;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] int playerLifes = 3;
    [SerializeField] float timeStun = 0.2f;
    [SerializeField] float powerUpDuration= 1f;
    [SerializeField] float timeFreezeDuration = 1.5f;
    [SerializeField] ParticleSystem dashFX;
    [SerializeField] ParticleSystem traceFX;
    [SerializeField] ParticleSystem playerWinPt;
    [SerializeField] ParticleSystem playerLosePt;
    [SerializeField] AudioClip jumpAudiFX;
    [SerializeField] AudioClip obstaclehitSoundFx;
    [SerializeField] AudioClip winSoundFX;
    [SerializeField] AudioClip loseSoundFx;
    [SerializeField] AudioClip CoinSoundFX;
    [SerializeField] AudioClip speedUpSound;
    [SerializeField] AudioClip FreezeSoundFX;
    AudioSource audioSource;
    GameManager gameManager;
    Animator anim;
    float horizontalInput;
    Rigidbody myRigidbd;
    bool isOnGround;

    bool isGamePlay;

    void Start()
    {
        playerRender = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        myRigidbd = GetComponent<Rigidbody>();
        speed = baseSpeed;
        isGamePlay = true;
    }

    
    void FixedUpdate()
    {
        if(!isGamePlay) return;
        PlayerMoveForward();
    }


    void Update()
    {
        if(!isGamePlay) return;
        PlayerInput();
        Die();
    }

    void PlayerMoveForward()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        myRigidbd.MovePosition(myRigidbd.position + forwardMove + horizontalMove);
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
            audioSource.PlayOneShot(jumpAudiFX);
            myRigidbd.velocity = new Vector3(0,jumpSpeed,0);
            isOnGround = false;
            dashFX.Stop();
            traceFX.Stop();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isOnGround = true;
            dashFX.Play();
            traceFX.Play();
        }
    }
    
    void OnParticleCollision(GameObject other)
    {
        ObstacleHit();
        Die();
    }

    IEnumerator PlayerStun()
    {
        yield return new WaitForSecondsRealtime(timeStun);
        speed = baseSpeed;
    }

    public void ObstacleHit()
    {
        audioSource.PlayOneShot(obstaclehitSoundFx);
        playerLifes--;
        gameManager.LivesText(playerLifes);
        speed = 0;
        StartCoroutine(PlayerStun());
    }

    public void Die()
    {

        if(playerLifes == 0)
        {
            dashFX.Stop();
            traceFX.Stop();
            isGamePlay = false;
        }
    }

    public void PowerUpActive()
    {
        speed = boostSpeed;
        audioSource.PlayOneShot(speedUpSound);
        StartCoroutine(PowerUpActivationTime());
    }

    IEnumerator PowerUpActivationTime()
    {
        yield return new WaitForSecondsRealtime(powerUpDuration);
        speed = baseSpeed;
    }

    public void PlayerWin()
    {
        audioSource.PlayOneShot(winSoundFX);
        isGamePlay = false;
        anim.SetBool("isHappy",true);
        playerWinPt.Play();
        dashFX.Stop();
        traceFX.Stop();
        
    }

    public void PlayerLose()
    {
        audioSource.PlayOneShot(loseSoundFx);
        playerLosePt.Play();
        dashFX.Stop();
        traceFX.Stop();
        anim.SetBool("LoseAni",true);
        isGamePlay = false;
    }

    public void PlayCoinSoundFX()
    {
        audioSource.PlayOneShot(CoinSoundFX);
    }


    public void Freeze()
    {
        audioSource.PlayOneShot(FreezeSoundFX);
        playerRender.sharedMaterial = playerskins[0];
        speed = 0;
        StartCoroutine(ReturnNormal());
    }

    IEnumerator ReturnNormal()
    {
        yield return new WaitForSecondsRealtime(timeFreezeDuration);
        playerRender.sharedMaterial = playerskins[1];
        speed = baseSpeed;
    }
}
