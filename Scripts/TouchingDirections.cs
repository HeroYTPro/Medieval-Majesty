using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public ContactFilter2D decorationFilter;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    public float decorationDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    RaycastHit2D[] decorationHits = new RaycastHit2D[5]; // Добавьте массив для декораций

    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded { get {
            return _isGrounded;
        } private set {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    [SerializeField]
    private bool _isOnDecoration;

    public bool IsOnDecoration
    {
        get
        {
            return _isOnDecoration;
        }
        private set
        {
            _isOnDecoration = value;
            animator.SetBool(AnimationStrings.isOnDecoration, value);
        }
    }


    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        //IsOnDecoration = touchingCol.Cast(Vector2.down, castFilter, groundHits, decorationDistance) > 0;
        IsOnDecoration = touchingCol.Cast(Vector2.down, decorationFilter, decorationHits, decorationDistance) > 0; // Используйте отдельный фильтр для декораций
    }

    //private void FixedUpdate()
    //{

    //}
}
