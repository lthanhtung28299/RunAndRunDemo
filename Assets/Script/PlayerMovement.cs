using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed = 7f;
    [SerializeField] float boostSpeed = 15f;
    float speed;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] int playerLifes = 3;
    [SerializeField] float timeStun = 0.2f;
    [SerializeField] float zRange = 5f;
    [SerializeField] float powerUpDuration= 1f;
    [SerializeField] ParticleSystem dashFX;
    [SerializeField] ParticleSystem traceFX;
    float horizontalInput;
    Rigidbody myRigidbd;
    bool isOnGround;
    bool isAlive = true;

    void Start()
    {
        myRigidbd = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    
    void FixedUpdate()
    {
        if(!isAlive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        myRigidbd.MovePosition(myRigidbd.position + forwardMove + horizontalMove);
    }


    void Update()
    {
        if(!isAlive) return;
        PlayerInput();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
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

    IEnumerator PlayerStun()
    {
        yield return new WaitForSecondsRealtime(timeStun);
        speed = baseSpeed;
    }

    public void ObstacleHit()
    {
        playerLifes--;
        speed = 0;
        StartCoroutine(PlayerStun());
    }

    public void Die()
    {
        if(playerLifes == 0)
        {
            dashFX.Stop();
            traceFX.Stop();
            isAlive = false;
        }
    }

    public void PowerUpActive()
    {
        speed = boostSpeed;
        StartCoroutine(PowerUpActivationTime());
    }

    IEnumerator PowerUpActivationTime()
    {
        yield return new WaitForSecondsRealtime(1);
        speed = baseSpeed;
    }
}
