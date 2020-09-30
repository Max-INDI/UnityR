using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPin : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed = 20f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindObjectOfType<GM>().isGaemeOver)
        {
            GameObject.FindObjectOfType<GM>().RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Rotator")
        {
            rb.velocity = Vector2.zero;
            if (Random.Range(0f, 1f) > 0.5f){
                col.GetComponent<Rotate>().speed *= -1;
            }
            if (Random.Range(0f, 1f) > 0.5f)
            {
                if (col.GetComponent<Rotate>().speed <= 180)
                    col.GetComponent<Rotate>().speed *= 1.5f;
            }
            Score.score++;
            transform.SetParent(col.transform);
        }
        else if (col.tag == "Pin")
        {
            GameObject.FindObjectOfType<GM>().GameOver();
        }
    }
}
