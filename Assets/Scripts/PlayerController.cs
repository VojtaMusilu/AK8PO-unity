using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private bool isMoving;
    private Vector2 input;
    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void Update()
    {
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        isMoving = true;
        animator.SetBool("isWalking", isMoving);
        rb.MovePosition(rb.position + movement * Time.deltaTime * playerSpeed);
        isMoving = false;
        animator.SetBool("isWalking", isMoving);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit by enemy");
    }
}
