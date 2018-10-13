using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dist = Mathf.Abs(Screen.height / 2 - Camera.main.WorldToScreenPoint(player.transform.position).y);

		if(dist > Screen.height/4)
		{
			transform.Translate(0, Mathf.Lerp(transform.position.y,player.transform.position.y, .6f), 0);
		}
	}
}
