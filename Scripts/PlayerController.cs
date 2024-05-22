using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    Vector2 checkpointPos;

    private float groundOffset = 0.5f; // Смещение относительно земли при возрождении

    TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentMoveSpeed { get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded || touchingDirections.IsOnDecoration)
                    {
                        if (isRunning)
                        {
                            return runSpeed;
                        }
                        else 
                        { 
                            return walkSpeed; 
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            } 
            else 
            { 
                return 0; 
            }
        } 
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        } 
        private set 
        {
            _isMoving = value;
            //animator.SetBool("isMoving", value);
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool isRunning 
    {  
        get
        {
            return _isRunning;
        } 
        set 
        { 
            _isRunning = value;
            //animator.SetBool("isRunning", value);
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set {
    
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        
        } 
    }

    public bool CanMove 
    { 
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } 
    }

    public bool IsAlive
    {
        get 
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    //public bool LockVelocity { 
    //    get
    //    {
    //        return animator.GetBool(AnimationStrings.lockVelocity);
    //    }
    //    set
    //    {
    //        animator.SetBool(AnimationStrings.lockVelocity, value);
    //    }
    //}

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private bool gameOverTriggered = false;

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (!damageable.IsAlive && !gameOverTriggered)
        {
            gameOverTriggered = true; // Устанавливаем флаг, чтобы предотвратить повторное выполнение
                                      // Сохраняем информацию о текущем уровне
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            SceneController.instance.GameOver();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //Повернуться направо
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //Повернуться налево
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && (touchingDirections.IsGrounded || touchingDirections.IsOnDecoration) && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void Start()
    {
        checkpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
            
            //SceneController.instance.GameOver();
            
            //SceneManager.LoadScene("GameOverScreen");
        }
    }

    public void death()
    {
        if(damageable.IsAlive == false)
        {
            SceneController.instance.GameOver();
        }
    }

    void Die()
    {
        //SceneController.instance.GameOver();
        StartCoroutine(Respawn(0.5f));
    }
    IEnumerator Respawn(float duration)
    {
        //rb.simulated = false;
        yield return new WaitForSeconds(duration);
        transform.position = new Vector2(checkpointPos.x, checkpointPos.y + groundOffset);
    }

    public void UpdateCheckpoint(Vector2 pos, float groundHeight)
    {
        checkpointPos = new Vector2(pos.x, groundHeight);
    }

    //private void Update()
    //{
    //    if (damageable.IsAlive == false)
    //    {
    //        SceneController.instance.GameOver();
    //    }
    //}
}
