using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpAccel;

    [Header("Ground RayCast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private Rigidbody2D rig;
    private Animator anim;
    private CharacterSoundsController sound;

    private bool isJumping;
    private bool isOnGround;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundsController>();
    }

    private void Update()
    {
        //read input
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;

                sound.PlayJump();
            }
        }

        //chage animation
    anim.SetBool("isOnGround", isOnGround);
    }

    private void FixedUpdate()
    {
        //Raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (isOnGround && rig.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }

        //calculate velocity vector
    
        Vector2 velocityVector = rig.velocity;

        if (isJumping)
        {
            velocityVector.y += jumpAccel;
            isJumping = true;
        }

        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

            rig.velocity = velocityVector;
        }
    }



  



