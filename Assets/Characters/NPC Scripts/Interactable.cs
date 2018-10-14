using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public bool interacting = false;
	public string interaction_string;
	public Sprite portrait;
	[SerializeField]
	GameObject interact_icon;
	public int random_responses;

	public void Interact()
	{
		NPCController controller = transform.GetComponent<NPCController>();
		if(controller)
		{
			controller.interacting = true;
		}
		interacting = true;
	}
	public void EndInteract()
	{
		NPCController controller = transform.GetComponent<NPCController>();
		if (controller)
		{
			controller.interacting = false;
		}
		interacting = false;
	}
	public void ShowInteractableIcon()
	{
		interact_icon.SetActive(true);
	}
	public void HideInteractableIcon()
	{
		interact_icon.SetActive(false);
	}
}
