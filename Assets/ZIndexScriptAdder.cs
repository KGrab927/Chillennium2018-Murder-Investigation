using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndexScriptAdder : MonoBehaviour {
	void Start () {
		foreach (Transform child in transform)
		{
			child.AddComponent<ZIndexFixer>();
		}
	}
}
