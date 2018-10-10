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

    public bool acceptInputs;
    public bool isDead;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<Rigidbody>();
        points = 0;
        acceptInputs = false;
        isDead = false;

        totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void FixedUpdate()
    {
        if (points < totalPickups && acceptInputs && !isDead)
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

        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy2")))
        {
            isDead = true;
        }
    }
}
