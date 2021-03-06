﻿using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour {
	private bool active;
	private bool nextEnd;
	private Dialogues dialogues;
	private bool showingText;
	private Interactable interactable;
	[SerializeField]
    Text dialogueText;
    [SerializeField]
    Text topText;
	[SerializeField]
	Text middleText;
	[SerializeField]
    Text bottomText;
    [SerializeField]
    GameObject dialogueUI;
	[SerializeField]
	GameObject buttonContainer;
	[SerializeField]
	GameObject dialogueContainer;
	[SerializeField]
	GameObject portrait;
	[SerializeField]
	GameObject portraitItem;


	void Start()
	{
		dialogueUI.SetActive(false);
		buttonContainer.SetActive(false);
		dialogueContainer.SetActive(true);
		active = false;
		showingText = true;
	}

	public void Hide()
	{
		dialogueUI.SetActive(false);
		buttonContainer.SetActive(false);
		dialogueContainer.SetActive(true);
		active = false;
		showingText = true;
		if (FindObjectsOfType<PlayerController>().Length > 0) {
			FindObjectsOfType<PlayerController>()[0].EndInteract();
		}
		interactable.EndInteract();
	}

	public void StartInteraction(GameObject obj, string str, int random_responses, int index = -1)
	{
		active = true;
		interactable = obj.GetComponent<Interactable>();
		if(interactable.isPerson)
		{
			portrait.GetComponent<Image>().sprite = interactable.portrait;
			portraitItem.SetActive(false);
			portrait.SetActive(true);
		}
		else
		{
			portraitItem.GetComponent<Image>().sprite = interactable.portrait;
			portraitItem.SetActive(true);
			portrait.SetActive(false);
		}
		interactable.Interact();
		dialogues = interactable.GetComponent<Dialogues>();
		if (index == -1) {
			if (random_responses > 0)
			{
				int ind = Random.Range(0, random_responses);
				dialogues.SetTree(ind.ToString());
			}
			else
			{
				dialogues.SetTree(str);
			}
		}
		else {
			dialogues.SetTree(index.ToString());
		}
		dialogues.Reset();
		dialogueText.text = dialogues.GetCurrentDialogue();
		dialogueUI.SetActive(true);
		
	}
	
	public void ShowOther()
	{
		//Currently showing text, wanting to show buttons
		if (showingText)
		{
			topText.text = dialogues.GetChoices()[0];
			if (dialogues.GetChoices().Length > 1)
				middleText.text = dialogues.GetChoices()[1];
			if (dialogues.GetChoices().Length > 2)
				bottomText.text = dialogues.GetChoices()[2];

			if (dialogues.GetChoices().Length > 1)
				middleText.transform.parent.gameObject.SetActive(true);
			else
				middleText.transform.parent.gameObject.SetActive(false);

			if (dialogues.GetChoices().Length > 2)
				bottomText.transform.parent.gameObject.SetActive(true);
			else
				bottomText.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			dialogueText.text = dialogues.GetCurrentDialogue();
		}

		showingText = !showingText;
		buttonContainer.SetActive(!showingText);
		dialogueContainer.SetActive(showingText);
	}

	public void Update()
	{
		if(!active)
		{
			return;
		}
		nextEnd = dialogues.End();
		if (dialogues.GetChoices().Length != 0)
		{
			if (showingText)
			{
				ShowOther();
			}
			if (Input.GetButtonDown("Choice 1"))
			{
				Debug.Log("Choice 1");
				Choice(0);
			}
			else if (Input.GetButtonDown("Choice 2"))
			{
				Debug.Log("Choice 2");
				Choice(1);
			}
			else if (Input.GetButtonDown("Choice 3"))
			{
				Debug.Log("Choice 3");
				Choice(2);
			}
		}
		else
		{
			if (!showingText)
			{
				ShowOther();
			}
			if (Input.GetButtonDown("Interact"))
			{
				if (!nextEnd)
				{
					dialogues.Next();
					dialogueText.text = dialogues.GetCurrentDialogue();
				}
				else
				{
					Hide();
				}
			}
		}
	}

	public void Choice(int index)
    {
		if (index < dialogues.GetChoices().Length)
		{
			try
			{
				dialogues.NextChoice(dialogues.GetChoices()[index]);
			} catch
			{
				Hide();
			}
		}
    }
}
