using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    static bool isSitting = false;
    bool isFacingRight = true;

    [SerializeField] float runSpeed = 10f;
    Health health;
    Animator animator;
  

    Weapon weapon;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    
    
    void Start()
    {

        health = GetComponent<Health>();
        weapon = GetComponent<Weapon>();
       

        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        Run();
        FlipSprite();
       
    }

    void OnJump(InputValue value)
    {

        if (health.returnAliveStatus() == false || isSitting == true || weapon.returnHoldingGunStatus()) { return; }
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            Debug.Log("jump");
            myRigidbody.velocity += new Vector2(0f, 10f);
        }
    }

    void OnFire(InputValue value)
    {
        if (weapon.returnHoldingGunStatus() ||  health.returnAliveStatus() == false)
        {
            if (value.isPressed)
            {
                weapon.Shoot();
            }
        }
    }
    void OnMove(InputValue value)
    {
        if (health.returnAliveStatus() == false || isSitting == true) { return; }



        moveInput = value.Get<Vector2>();

    }

    void OnSit(InputValue value)
    {

        if (health.returnAliveStatus() == false) { return; }

        if (value.isPressed)
        {

            isSitting = !isSitting;

            if (isSitting == true)
            {
                animator.SetBool("isSitting", true);
            }

            if (isSitting == false)
            {
                animator.SetBool("isSitting", false);
            }
        }



    }

    void Run()
    {
        if (health.returnAliveStatus() == false || isSitting == true || weapon.returnHoldingGunStatus()) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontal = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;


        animator.SetBool("isRunning", playerHasHorizontal);


    }
    void FlipSprite()
    {
        if (health.returnAliveStatus() == false || isSitting == true || weapon.returnHoldingGunStatus() == true) { return; }
        if (isFacingRight && myRigidbody.velocity.x < 0f || !isFacingRight && myRigidbody.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            
        }
    }


   








}
