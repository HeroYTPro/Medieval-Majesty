using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public GameObject points;
    public Transform[] wayPoints;

    private int pointIndex;
    private int direction = 1;
    private Vector3 targetpos;

    public float waitDuration;
    private bool isWaiting;

    private void Awake()
    {
        wayPoints = new Transform[points.transform.childCount];
        for (int i = 0; i < points.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = points.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointIndex = 1;
        targetpos = wayPoints[1].transform.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            if (Vector2.Distance(transform.position, targetpos) < 0.1f)
            {
                StartCoroutine(WaitAndMove());
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetpos, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator WaitAndMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitDuration);
        isWaiting = false;
        NextPoint();
    }

    public void NextPoint()
    {
        if (pointIndex == wayPoints.Length - 1)
        {
            direction = -1;
        }
        else if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetpos = wayPoints[pointIndex].transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
