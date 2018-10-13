using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed;            
	
	private Rigidbody2D rb2d;     

	
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.freezeRotation = true;
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		
		if(movement.magnitude == 0)
		{
			rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, 0, .8f), Mathf.Lerp(rb2d.velocity.y, 0, .8f));
		}
		else
		{
			rb2d.velocity = (movement.normalized * speed);
		}

	}
}