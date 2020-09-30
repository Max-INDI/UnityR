using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPin : MonoBehaviour {
    public GameObject pin;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Spawn();
        }
	}

    void Spawn()
    {
        Instantiate(pin, transform.position, transform.rotation);
    }
}
