using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour {

    [SerializeField] string actionButtonName = "Kryds";
    TextMesh statusTxt;

	void Start () {
        statusTxt = GetComponentInChildren<TextMesh>();
        statusTxt.text = "Tryk " + actionButtonName;
	}
	
    public void SetStatusText(string _txt) {
        statusTxt.text = _txt;
    }
}
