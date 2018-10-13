using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public string interaction_string;
	public Sprite portrait;

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
