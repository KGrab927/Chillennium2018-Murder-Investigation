using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenningLevelLoader : MonoBehaviour
{
	public void ZOnFadeComplete()
	{
		SceneManager.LoadScene("main", LoadSceneMode.Single);
	}
}
