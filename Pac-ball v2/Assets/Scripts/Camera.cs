using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    private Vector3 velocity = Vector3.zero;
    private float fixedY;

    private void Start()
    {
        fixedY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.1f);
        newPos.y = fixedY;
        transform.position = newPos;
	}
}
