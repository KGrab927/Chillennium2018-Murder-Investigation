using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockController : MonoBehaviour {
	[SerializeField]
	GameObject minuteHand;
	[SerializeField]
	GameObject hourHand;
	[SerializeField]
	float minutesToEnd;
	[SerializeField]
	Animator anim;

	// Update is called once per frame
	void Update () {
		minuteHand.transform.Rotate(new Vector3(0,0,-1), ((180 / minutesToEnd) / 60) * Time.deltaTime);
		hourHand.transform.Rotate(new Vector3(0, 0, -1), ((15 / minutesToEnd) / 60) * Time.deltaTime);

		if(hourHand.transform.rotation.z >= 90)
		{
			anim.SetTrigger("FadeOut");
		}
	}	
}
