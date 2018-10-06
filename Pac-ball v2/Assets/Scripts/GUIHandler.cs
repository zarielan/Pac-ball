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

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    private void Update()
    {
        while (totalPickups <= 0)
            totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;

        pickedUp = player.points;
        ui.text = string.Format("Time: {0:0.00}s{1}Points: {2} / {3} ({4:0.00}%)", Time.time, Environment.NewLine, pickedUp, totalPickups, pickedUp / totalPickups * 100);
    }
}
