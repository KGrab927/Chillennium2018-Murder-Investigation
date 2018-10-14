using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if(hourHand.transform.rotation.eulerAngles.z > 90)
		{
			minuteHand.transform.Rotate(new Vector3(0, 0, -1), ((180 / minutesToEnd) / 60) * Time.deltaTime);
			hourHand.transform.Rotate(new Vector3(0, 0, -1), ((15 / minutesToEnd) / 60) * Time.deltaTime);
		}
		else
		{
			minuteHand.transform.eulerAngles = new Vector3(
			   minuteHand.transform.eulerAngles.x,
			   minuteHand.transform.eulerAngles.y,
			   90
		   );
			hourHand.transform.eulerAngles = new Vector3(
				minuteHand.transform.eulerAngles.x,
				minuteHand.transform.eulerAngles.y,
				-270
			);
			anim.SetTrigger("FadeOut");
		}
	}	
}
