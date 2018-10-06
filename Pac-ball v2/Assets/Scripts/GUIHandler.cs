using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour
{
    public Text time;

    private void Start()
    {
        
    }

    private void Update()
    {
        time.text = string.Format("Time: {0:0.00}s", Time.time);
    }
}
