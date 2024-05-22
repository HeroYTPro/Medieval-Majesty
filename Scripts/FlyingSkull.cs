using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSkull : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;

    public float followSpeed = 5f;
    public Transform player;
    public Collider2D followZone;
    public float followDistance = 3f; // ћинимальное рассто€ние между противником и игроком

    private bool isFollowing = false;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTaret = false;
    public bool HasTarget
    {
        get { return _hasTaret; }
        private set
        {
            _hasTaret = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;

        if (followZone.IsTouching(player.GetComponent<Collider2D>()))
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }
    }

    private void FixedUpdate()
    {
        //if (damageable.IsAlive)
        //{
        //    if (CanMove)
        //    {
        //        Flight();
        //    }
        //    else
        //    {
        //        rb.velocity = Vector3.zero;
        //    }
        //}

        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                if (isFollowing)
                {
                    FollowPlayer();
                }
                else
                {
                    Flight();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void FollowPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // ѕровер€ем рассто€ние до игрока
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // ≈сли рассто€ние до игрока больше минимального преследовани€
        if (distanceToPlayer > followDistance)
        {
            // ѕротивник двигаетс€ в направлении игрока
            rb.velocity = directionToPlayer * followSpeed;
            UpdateDirection();
        }
        else
        {
            // ѕротивник находитс€ достаточно близко к игроку, можете сделать что-то еще,
            // например, атаковать игрока.
            // Ќапример:
            // AttackPlayer();
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
