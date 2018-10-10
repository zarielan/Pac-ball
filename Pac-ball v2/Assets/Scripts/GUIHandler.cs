using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GUIHandler : MonoBehaviour
{
    public Text ui;
    private Player player;

    private float totalPickups;
    private float pickedUp;

    public Image overlay;
    public Text endingText;
    public Text endingDescription;
    public CanvasGroup ending;
    public CanvasGroup start;

    private int state;
    private float currentTime;
    private float startTime;

    private void Start()
    {
        state = 0;
        player = GameObject.Find("Player").GetComponent<Player>();
        totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void Update()
    {
        if (player.isDead && state < 2)
        {
            state = 3;
        }

        if (state == 0)
        {
            if (Input.GetMouseButton(0))
            {
                start.alpha = 0f;
                start.blocksRaycasts = false;
                state++;
                player.acceptInputs = true;
                startTime = Time.time;
            }
        }
        if (state == 1)
        {
            while (totalPickups <= 0)
                totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;

            pickedUp = player.points;
            currentTime = Time.time - startTime;

            ui.text = string.Format("Time: {0:0.00}s{1}Points: {2} / {3} ({4:0.00}%)", currentTime, Environment.NewLine, pickedUp, totalPickups, pickedUp / totalPickups * 100);

            if (pickedUp >= totalPickups)
                state = 2;
        }
        else if (state == 2)
        {
            endingDescription.text = string.Format("You finished the game in {0:0.00} seconds!{1}{1}Click to start a new game!", currentTime, Environment.NewLine);
            endingText.text = "You Won! :D";

            ending.alpha = 1f;
            ending.blocksRaycasts = true;
        }
        else if (state == 3)
        {
            endingDescription.text = string.Format("It's okay, you got {0} of {1} points in {2:0.00} seconds tho!{3}{3}Click to start a new game!", pickedUp, totalPickups, currentTime, Environment.NewLine);
            endingText.text = "You Died! :(";

            ending.alpha = 1f;
            ending.blocksRaycasts = true;
        }

        if (Input.GetMouseButton(0) && state >= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
