using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject PassTheBombUI;

    [SerializeField] GameObject statusTxt;

    private void Start() {
        PassTheBombUI.SetActive(false);
    }

    public void StartGame() {
        //MainMenuUI.StartGame();
    }

    public void SetStatusText(string _txt) {
        statusTxt.gameObject.SetActive(true);
        statusTxt.GetComponentInChildren<Text>().text = _txt;
        statusTxt.GetComponent<Animator>().SetTrigger("Show");
    }

}
