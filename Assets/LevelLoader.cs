﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
	public void OnFadeComplete()
	{
		Scene sceneToLoad = SceneManager.GetSceneByName("Level2");
		SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
	}
}
