using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startPosition;
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPosition;
    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startingZ = transform.position.z * 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
