using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

    public InputDevice Device { get; set; }
    public string playername;
    public int index;

    [SerializeField] TextMesh playernameText;

	void Start () {
		foreach(MonoBehaviour script in GetComponents(typeof(MonoBehaviour))) {
            script.enabled = false;
        }
	}
	
	void Update () {
		
	}

    public void SetPlayername(string _name) {
        playername = _name;
        playernameText.text = _name;
    }
}
