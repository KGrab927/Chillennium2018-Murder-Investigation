using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opening : MonoBehaviour {

	GameObject[] players;

	int currDialogIndex;

	string[] order;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		currDialogIndex = 0;
		order = new string[] {"Best Friend", "Priest", "Butler", "Dead Player"};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
