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

	[SerializeField]
	int seed;

	private float prev_time;

	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator>();
		rnd = new System.Random(seed);
		interacting = false;
		prev_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!interacting && Time.time - prev_time > 2) {
			int rnd1 = rnd.Next(0, 3) - 1;
			int rnd2 = rnd.Next(0, 3) - 1;

			ac.SetFloat("walk_dir_x", rnd1);
			ac.SetFloat("walk_dir_y", rnd2);
			prev_time = Time.time;
		}
		else {
			ac.SetFloat("walk_dir_x", x);
			ac.SetFloat("walk_dir_y", y);	
		}
	}
}
