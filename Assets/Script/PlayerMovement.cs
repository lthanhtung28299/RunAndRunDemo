using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    float horizontalInput;
    Rigidbody myRigidbd;

    void Start()
    {
        myRigidbd = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        myRigidbd.MovePosition(myRigidbd.position + forwardMove + horizontalMove);
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.Space) && )
        {
            myRigidbd.velocity = new Vector3(0,jumpSpeed,0);
        }
    }
}
