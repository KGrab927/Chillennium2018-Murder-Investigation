using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public int follow_speed;

	void FixedUpdate () {
		float dist = (Screen.height / 2 - Camera.main.WorldToScreenPoint(player.transform.position).y);

		if(Mathf.Abs(dist) > 100)
		{
			int dir = -1;
			if(dist < 0)
			{
				dir = 1;
			}
			transform.transform.position += new Vector3(0, dir * Time.fixedDeltaTime * Mathf.Lerp(0, follow_speed, Mathf.Abs(dist/(Screen.height/2))), 0);
		}
	}
}
