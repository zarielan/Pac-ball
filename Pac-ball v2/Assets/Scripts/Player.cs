using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody player;

    private Vector3 move;
    public float speed;
    public int points;

    private int totalPickups;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<Rigidbody>();
        points = 0;

        totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void FixedUpdate()
    {
        if (points < totalPickups)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            move = new Vector3(moveHorizontal, 0f, moveVertical);

            player.AddForce(move * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            points++;
        }
    }
}
