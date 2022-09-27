using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health = 10;
    Animator animator;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D capsuleCollider;
     SpriteRenderer render;
    //Transform transform;

    float movespeed = -5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
         render = GetComponent<SpriteRenderer>();
        //transform = GetComponent<Transform>();
    }

 
    void Update()
    {
        myRigidbody.velocity = new Vector2(movespeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            StartCoroutine(ChangeColor());
           
            Health -= 2;
            Debug.Log(Health);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Cage")
        {
            movespeed = -movespeed;
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        }
    }

 

    IEnumerator ChangeColor()
    {
        render.color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(0.5f);
        render.color = new Color(1, 1, 1);
    }



   

}