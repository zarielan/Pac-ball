using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    public float speed;
    private Vector3[] waypoints;
    private int i;
    private const float minDistance = 0f;

    private void Start()
    {
        i = 0;

        waypoints = new Vector3[] {
            new Vector3(-2f, 0.5f, -8f),
            new Vector3(-2f, 0.5f, -6f),
            new Vector3(0f, 0.5f, -6f),
            new Vector3(0f, 0.5f, -8f),
            new Vector3(8f, 0.5f, -8f),
            new Vector3(8f, 0.5f, -2f),
            new Vector3(6f, 0.5f, -2f),
            new Vector3(6f, 0.5f, 0f),
            new Vector3(8f, 0.5f, 0f),
            new Vector3(8f, 0.5f, 8f),
            new Vector3(2f, 0.5f, 8f),
            new Vector3(2f, 0.5f, 6f),
            new Vector3(0f, 0.5f, 6f),
            new Vector3(0f, 0.5f, 8f),
            new Vector3(-8f, 0.5f, 8f),
            new Vector3(-8f, 0.5f, 2f),
            new Vector3(-6f, 0.5f, 2f),
            new Vector3(-6f, 0.5f, 0f),
            new Vector3(-8f, 0.5f, 0f),
            new Vector3(-8f, 0.5f, -8f),
        };

        StartCoroutine(Move());
    }

    private void GetNewWaypoint()
    {
        int newWaypoint = i + 1;

        if (newWaypoint > waypoints.Length - 1)
            newWaypoint = 0;

        i = newWaypoint;

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float distance = Vector3.Distance(transform.position, waypoints[i]);

        while (distance > minDistance)
        {
            distance = Vector3.Distance(transform.position, waypoints[i]);

            transform.position = Vector3.MoveTowards(transform.position, waypoints[i], speed * Time.deltaTime);

            if (distance <= minDistance)
            {
                GetNewWaypoint();
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
