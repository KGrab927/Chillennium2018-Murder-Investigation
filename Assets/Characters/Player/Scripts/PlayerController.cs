using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float pickup_range;

	[SerializeField]
	DialogueInteraction dialogueInteraction;

	public float speed;            
	
	private Rigidbody2D rb2d;
	private Rigidbody2D p_rb2d;
	private Animator anim;
	private Animator p_anim;
	private GameObject person;
	float dir_x, dir_y;

	[HideInInspector]
	public bool interacting;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		rb2d.freezeRotation = true;
		person = null;
		interacting = false;
		dir_x = 0;
		dir_y = 0;
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		if (movement.magnitude == 0 || interacting)
		{
			rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, 0, .8f), Mathf.Lerp(rb2d.velocity.y, 0, .8f));
			anim.SetBool("moving", false);
		}
		else
		{
			rb2d.velocity = (movement.normalized * speed);
			anim.SetBool("moving", true);
			dir_x = moveHorizontal;
			dir_y = moveVertical;
			anim.SetFloat("walk_dir_x", moveHorizontal);
			anim.SetFloat("walk_dir_y", moveVertical);
		}

		GameObject[] interactables = null;
		GameObject closest = null;
		float min_dist = pickup_range;
		interactables = GameObject.FindGameObjectsWithTag("Possessable");
		foreach (GameObject i in interactables)
		{
			i.GetComponent<Interactable>().HideInteractableIcon();
			if (i == person)
			{
				continue;
			}
			Vector3 diff = (person == null) ? i.transform.position - transform.position : i.transform.position - person.transform.position;
			float dist = diff.magnitude;
			if (dist < pickup_range && dist < min_dist)
			{
				if (diff.x > 0 && dir_x > 0 ||
					diff.x < 0 && dir_x < 0 ||
					diff.y > 0 && dir_y > 0 ||
					diff.y < 0 && dir_y < 0)
				{
					min_dist = dist;
					closest = i;
				}
			}
		}
		interactables = GameObject.FindGameObjectsWithTag("Interactable");
		foreach (GameObject i in interactables)
		{
			i.GetComponent<Interactable>().HideInteractableIcon();
			if (i == person)
			{
				continue;
			}
			Vector3 diff = i.transform.position - ((person != null) ? person.transform.position : transform.position);
			float dist = diff.magnitude;
			if (dist < pickup_range && dist < min_dist)
			{
				if (diff.x > 0 && dir_x > 0 ||
					diff.x < 0 && dir_x < 0 ||
					diff.y > 0 && dir_y > 0 ||
					diff.y < 0 && dir_y < 0)
				{
					min_dist = dist;
					closest = i;
				}
			}
		}

		if (person != null && !interacting)
		{
			if(closest)
			{
				closest.GetComponent<Interactable>().ShowInteractableIcon();
			}
			if (closest && Input.GetButtonDown("Interact"))
			{
				Interactable inter = person.GetComponent<Interactable>();
				interacting = true;
				Input.ResetInputAxes();
				FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(closest, inter.interaction_string, closest.GetComponent<Interactable>().random_responses);
			}
		} 
		else if(person == null && !interacting)
		{
			if (closest && closest.GetComponent<NPCController>())
			{
				closest.GetComponent<Interactable>().ShowInteractableIcon();
			}
			if (closest && closest.tag == "Interactable" && Input.GetButtonDown("Interact") && closest.GetComponent<NPCController>()) {
				Interactable inter = closest.GetComponent<Interactable>();
				interacting = true;
				Input.ResetInputAxes();
				FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(closest, "nullperson", -1);
			}
			else if (closest && Input.GetButtonDown("Interact"))
			{
				person = closest;
				rb2d.isKinematic = true;
				anim.enabled = false;
				transform.position = closest.transform.position;
				rb2d.velocity = new Vector2(0, 0);
				transform.position = new Vector3(1000F, 1000F, -50F);
				p_rb2d = rb2d;
				p_anim = anim;

				anim = closest.GetComponent<Animator>();
				anim.enabled = true;
				rb2d = closest.GetComponent<Rigidbody2D>();
				rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
				person.GetComponent<NPCController>().enabled = false;
				FindObjectsOfType<CameraController>()[0].player = person;
				Input.ResetInputAxes();
			}
		}
		if (Input.GetButtonDown("Depossess") && person != null && !interacting) {
			EndPossess();
		}
		if (closest && person != null) {
			if (closest.transform.position.y > person.transform.position.y) {
				person.transform.position = new Vector3(person.transform.position.x, person.transform.position.y, -0.3f);
			}
			else {
				person.transform.position = new Vector3(person.transform.position.x, person.transform.position.y, -0.2f);
			}
		}
		else if (closest) {
			if (closest.transform.position.y > transform.position.y) {
				transform.position = new Vector3(transform.position.x, transform.position.y, -0.3f);
			}
			else {
				transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
			}
		}
	}
	public void EndInteract()
	{
		interacting = false;
	}
	public void EndPossess()
	{
		rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
		rb2d = p_rb2d;
		anim.enabled = false;
		p_anim.enabled = true;
		anim = p_anim;
		person.GetComponent<NPCController>().enabled = true;
		FindObjectsOfType<CameraController>()[0].player = gameObject;
		transform.position = person.transform.position + new Vector3(p_anim.GetFloat("walk_dir_x"), p_anim.GetFloat("walk_dir_y"), 0);

		person.transform.position = new Vector3(person.transform.position.x, person.transform.position.y, -0.21875F);

		person = null;
		rb2d.bodyType = RigidbodyType2D.Dynamic;
	}

	public float getPlayerAnimatorValue(string paramKey) {
		return anim.GetFloat(paramKey);
	}
}