using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour {

	[HideInInspector] public Animator animator;
	public new string name;
    public Renderer[] modelRenderers;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	public void Affraid() {
		animator.SetTrigger ("Affraid");
	}

	public void Mock() {
		animator.SetTrigger ("Mock");
	}

    public void SetupModel(Color _color) {
        foreach (Renderer ren in modelRenderers) {
            ren.material.color = _color;
        }

    }

}
