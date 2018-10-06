using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    private void Start()
    {
        state = 0;
        player = GameObject.Find("Player").GetComponent<Player>();
        totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void Update()
    {
        if (state == 0)
        {
            if (Input.GetMouseButton(0))
            {
                start.alpha = 0f;
                start.blocksRaycasts = false;
                state++;
                player.acceptInputs = true;
            }
        }
        if (state == 1)
        {
            while (totalPickups <= 0)
                totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;

            pickedUp = player.points;
            currentTime = Time.time;

            ui.text = string.Format("Time: {0:0.00}s{1}Points: {2} / {3} ({4:0.00}%)", currentTime, Environment.NewLine, pickedUp, totalPickups, pickedUp / totalPickups * 100);

            if (pickedUp >= totalPickups)
                state++;
        }
        else if ( state >= 2)
        {
            endingDescription.text = string.Format("You finished the game in {0:0.00} seconds!", currentTime);
            endingText.text = "You Won! :D";

            ending.alpha = 1f;
            ending.blocksRaycasts = true;
        }
    }
}
