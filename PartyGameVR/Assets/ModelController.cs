using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour {

	[HideInInspector] public Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}




}
