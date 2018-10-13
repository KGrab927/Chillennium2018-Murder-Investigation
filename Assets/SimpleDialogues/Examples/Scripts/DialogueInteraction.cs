using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour {
	private bool active;
	private bool nextEnd;
	private Dialogues dialogues;
	private bool showingText;
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

    
	void Start() {
		Hide();
	}

	public void Hide()
	{
		dialogueUI.SetActive(false);
		buttonContainer.SetActive(false);
		active = false;
		showingText = true;
	}

	public void StartInteraction(GameObject obj)
	{
		active = true;
		Interactable interactable = obj.GetComponent<Interactable>();
		portrait.GetComponent<Image>().sprite = interactable.portrait;
		interactable.Interact();
		dialogues = interactable.GetComponent<Dialogues>();
		dialogues.SetTree("interaction");
	}
	
	public void ShowOther()
	{
		//Currently showing text, wanting to show buttons
		if (showingText)
		{
			topText.text = dialogues.GetChoices()[0];
			middleText.text = dialogues.GetChoices()[1];
			if (dialogues.GetChoices().Length > 2)
				bottomText.text = dialogues.GetChoices()[2];
			
			if (dialogues.GetChoices().Length > 2)
				bottomText.transform.parent.gameObject.SetActive(true);
			else
				bottomText.transform.parent.gameObject.SetActive(false);
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
			if (Input.GetKeyDown("Choice 1"))
			{
				Choice(0);
			}
			else if (Input.GetKeyDown("Choice 2"))
			{
				Choice(1);
			}
			else if (Input.GetKeyDown("Choice 3"))
			{
				Choice(2);
			}
		}
		else
		{
			if (!showingText)
			{
				ShowOther();
			}
			if (Input.GetKeyDown("Interact"))
			{
				if(!nextEnd)
				{
					dialogues.Next();
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
        if(index < dialogues.GetChoices().Length)
		{
			dialogues.NextChoice(dialogues.GetChoices()[index]);
		}
    }
}
