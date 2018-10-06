using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject pickupPrefab;

	// Use this for initialization
	void Start()
    {
	    for (int x = -8; x <= 8; x += 2)
        {
            for (int z = -8; z <= 8; z += 2)
            {
                if (x == 0 && z == 0)
                    continue;

                GameObject pickupGeneric = Instantiate(pickupPrefab);
                pickupGeneric.transform.position = new Vector3(x, 0.5f, z);
                pickupGeneric.transform.Rotate(new Vector3(45, 45, 45));
                pickupGeneric.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
