using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour {
	private Dialogues dialogues;
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

	bool nextEnd = false;
	

    
	void Start() {
		Hide();
	}

	public void Hide()
	{
		dialogueUI.SetActive(false);
	}

	public void StartConversation(GameObject NPC)
	{
		portrait.GetComponent<Image>().sprite = NPC.GetComponent<NPCData>().portrait;
		dialogues = NPC.GetComponent<Dialogues>();
	}

    public void Choice(int index)
    {
        if (index == 2 && dialogues.GetCurrentTree() == "TalkAgain") index = 1;
        if (dialogues.GetChoices().Length != 0)
        {
            dialogues.NextChoice(dialogues.GetChoices()[index]); //We make a choice out of the available choices based on the passed index.
            Display();                               //We actually call this function on the left and right button's onclick functions
        }
        else
        {
            Progress();
        }
    }

    public void TalkAgain()
    {
        dialogues.SetTree("TalkAgain");
        nextEnd = false;
        Display();
    }

    public void Progress()
    {
        dialogues.Next(); //This function returns the number of choices it has, in my case I'm checking that in the Display() function though.
        Display();
    }

    public void Display()
    {
        if (nextEnd == true)
        {
            dialogueUI.SetActive(false);
        }
        else
        {
            dialogueUI.SetActive(true);
        }

        //Sets our text to the current text
        dialogueText.text = dialogues.GetCurrentDialogue();
        //Just debug log our triggers for example purposes
        if (dialogues.HasTrigger())
            Debug.Log("Triggered: "+dialogues.GetTrigger());
        //This checks if there are any choices to be made
        if (dialogues.GetChoices().Length != 0)
        {
            //Setting the text's of the buttons to the choices text, in my case I know I'll always have a max of three choices for this example.
            topText.text = dialogues.GetChoices()[0];
            middleText.text = dialogues.GetChoices()[1];
            //If we only have two choices, adjust accordingly
            if (dialogues.GetChoices().Length > 2)
                bottomText.text = dialogues.GetChoices()[2];
            else
                bottomText.text = dialogues.GetChoices()[1];
            //Setting the appropriate buttons visability
            topText.transform.parent.gameObject.SetActive(true);
            bottomText.transform.parent.gameObject.SetActive(true);
            if(dialogues.GetChoices().Length > 2)
                middleText.transform.parent.gameObject.SetActive(true);
            else
                middleText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            middleText.text = "Continue";
            //Setting the appropriate buttons visability
            topText.transform.parent.gameObject.SetActive(false);
            bottomText.transform.parent.gameObject.SetActive(false);
            middleText.transform.parent.gameObject.SetActive(true);
        }
        
        if (dialogues.End()) //If this is the last dialogue, set it so the next time we hit "Continue" it will hide the panel
            nextEnd = true;
    }
}
