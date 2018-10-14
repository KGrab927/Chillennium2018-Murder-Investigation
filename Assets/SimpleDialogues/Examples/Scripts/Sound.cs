using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sound : MonoBehaviour {

    public AudioClip playerSelect;
    public AudioSource playerSelSource;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void playClicked()
    {
        playerSelSource.clip = playerSelect;
        playerSelSource.Play();

    }


}

