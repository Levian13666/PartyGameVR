using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SVGImporter;

public class UIKey : MonoBehaviour {

    [SerializeField] string Letter;

    void Start() {
        if (Letter != null) {
            SetLetter(Letter);
        }
    }

    public void SetLetter(string _letter) {
        GetComponentInChildren<Text>().text = _letter.ToUpper();
    }
}