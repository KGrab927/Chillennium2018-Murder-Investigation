using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public Sprite portrait;		

	public void Interact()
	{
		NPCController controller = transform.parent.GetComponent<NPCController>();
		if(controller)
		{
			controller.interacting = true;
		}
	}
}
