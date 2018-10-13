using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public Sprite portrait;
	public string interaction_string;

	public void Interact()
	{
		NPCController controller = transform.GetComponent<NPCController>();
		if(controller)
		{
			controller.interacting = true;
		}
	}
	public void EndInteract()
	{
		NPCController controller = transform.GetComponent<NPCController>();
		if (controller)
		{
			controller.interacting = false;
		}
	}
}
