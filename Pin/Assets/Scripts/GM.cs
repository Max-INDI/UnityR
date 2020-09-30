using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GM : MonoBehaviour {
    public bool isGaemeOver = false;
    public SpawnPin spawner;
    public Rotate rotation;
	// Use this for initialization
	void Start () {
		
	}
	    
    public void GameOver()
    {
        if (!isGaemeOver)
        {
            isGaemeOver = true;
            spawner.enabled = false;
            rotation.enabled = false;
            GetComponent<Animator>().SetTrigger("GameOver");
        }
    }

    public void RestartGame()
    {
        if(Input.GetButtonDown("Fire1"))
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
