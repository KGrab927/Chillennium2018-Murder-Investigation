using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opening : MonoBehaviour {

	GameObject[] players;
	GameObject deadPlayer;

	int currDialogIndex;
	bool spin;

	string[] order;

	[SerializeField]
	Animator anim;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		currDialogIndex = 0;
		order = new string[] {"Best Friend", "Butler", "Priest"};
		deadPlayer = GameObject.Find("Dead Player");
		spin = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (spin) {
			if (currDialogIndex < 3) {
				GameObject currPerson = GameObject.Find(order[currDialogIndex]);
				FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(currPerson, order[currDialogIndex].ToLower(), 0);
				if (Input.GetButtonDown("Interact")) {
					currDialogIndex++;
				}
			}
			else if (spin){
				var rot = deadPlayer.transform.rotation;
				rot.z += Time.deltaTime * 2;
				deadPlayer.transform.rotation = rot;
				
			}

			if (deadPlayer.transform.eulerAngles.z > 170 && deadPlayer.transform.eulerAngles.z < 269 && currDialogIndex == 3) {
				spin = false;
				currDialogIndex = 0;
				deadPlayer.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.LoadAll("walkSprite", typeof(Sprite))[0];
				deadPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		else if (currDialogIndex == 0){
			FindObjectsOfType<DialogueInteraction>()[0].StartInteraction(deadPlayer, "dead player", 0, 0);
			currDialogIndex++;
		}

		if(!spin && currDialogIndex == 1 && !deadPlayer.GetComponent<Interactable>().interacting)
		{
			anim.SetTrigger("FadeOut");
		}



	}
}
