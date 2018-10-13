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
	private Animator anim;
	private string person;
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

		if (movement.magnitude == 0)
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

		if(Input.GetButtonDown("Interact") && person != null && !interacting)
		{
			GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
			GameObject closest = null;
			float min_dist = pickup_range;
			foreach (GameObject i in interactables)
			{
				Vector3 diff = i.transform.position - transform.position;
				float dist = diff.magnitude;
				Debug.Log(interactables.Length);
				Debug.Log(dist < pickup_range);
				if (dist < pickup_range && dist < min_dist)
				{
					if(diff.x > 0 && dir_x > 0 ||
					   diff.x < 0 && dir_x < 0 ||
					   diff.y > 0 && dir_y > 0 ||
					   diff.y < 0 && dir_y < 0)
					{
						min_dist = dist;
						closest = i;
					}
				}
			}

			if (closest)
			{
				interacting = true;
				Input.ResetInputAxes();
				FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(closest);
			}
		} 
		else if(Input.GetButtonDown("Interact") && person == null && !interacting)
		{

			GameObject[] interactables = GameObject.FindGameObjectsWithTag("Possessable");
			GameObject closest = null;
			float min_dist = pickup_range;
			foreach (GameObject i in interactables)
			{
				Vector3 diff = i.transform.position - transform.position;
				float dist = diff.magnitude;
				Debug.Log(interactables.Length);
				Debug.Log(dist < pickup_range);
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

			if (closest)
			{
				person = null; 
				Input.ResetInputAxes();
				FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(closest);
			}
		}
	}
	public void EndInteract()
	{
		interacting = false;
	}
	public void EndPossess()
	{
		person = null;
	}
}