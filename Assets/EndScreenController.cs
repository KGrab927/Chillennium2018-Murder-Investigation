using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {
	[SerializeField]
	GameObject newsScene;
	[SerializeField]
	GameObject pickScene;
	[SerializeField]
	GameObject newspaper;
	[SerializeField]
	Sprite dutchess;
	[SerializeField]
	Sprite chef;
	[SerializeField]
	Sprite money;
	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(pickScene.activeSelf)
		{
			if (Input.GetButtonDown("Choice 1"))
			{
				newspaper.GetComponent<Image>().sprite = chef;
				anim.SetTrigger("FadeOut");
			}
			else if (Input.GetButtonDown("Choice 2"))
			{
				newspaper.GetComponent<Image>().sprite = money;
				anim.SetTrigger("FadeOut");
			}
			else if (Input.GetButtonDown("Choice 3"))
			{
				newspaper.GetComponent<Image>().sprite = dutchess;
				anim.SetTrigger("FadeOut");
			}
		}
		else
		{
			if (Input.GetButtonDown("Interact"))
			{
				newspaper.GetComponent<Image>().sprite = chef;
				anim.SetTrigger("FadeOut");
			}
		}
		
	}

	public void ZOnFadeOut()
	{
		if (pickScene.activeSelf)
		{
			newsScene.SetActive(true);
			pickScene.SetActive(false);
			anim.SetTrigger("FadeIn");
		} else
		{
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		}

	}



}
