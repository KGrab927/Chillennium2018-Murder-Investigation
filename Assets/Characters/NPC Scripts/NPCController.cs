using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Animations;

public class NPCController : MonoBehaviour {
	[HideInInspector]
	public bool interacting;
	private System.Random rnd;
	private Animator ac;

	private float prev_time;

	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator>();
		rnd = new System.Random();
		interacting = false;
		prev_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!interacting && Time.time - prev_time > 2) {
			int rnd1 = (rnd.Next(0, 2) == 1) ? -1 : 1;
			int rnd2 = (rnd.Next(0, 2) == 1) ? 1 : -1;
			ac.SetFloat("walk_dir_x", rnd1);
			ac.SetFloat("walk_dir_y", rnd2);
			prev_time = Time.time;
		}
	}
}
