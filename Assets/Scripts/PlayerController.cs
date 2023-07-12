using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Vector2 input;
    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rb;
    public Timer timer;
    public EndDialog dialog;
    public Object enemy;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * playerSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        timer.running = false;
        Debug.Log($"hit by enemy, timer: {timer.currentTime}");
        dialog.gameObject.SetActive(true);
        dialog.tryAgain.onClick.AddListener(TryAgainClicked);
    }
    
    private void TryAgainClicked()
    {
        rb.position = new Vector2(38, 4);
        enemy.GameObject().gameObject.transform.position = new Vector3(22, -1);
        timer.currentTime = 0;
        timer.running = true;
        dialog.gameObject.SetActive(false);
    }
}
