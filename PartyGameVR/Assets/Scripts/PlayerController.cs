using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

    public InputDevice Device { get; set; }
    public string playername;
    public int index;
	public int colorIndex = 0;
	public Color color;

    [SerializeField] TextMesh playernameText;
	[SerializeField] GameObject model;

	GameController gameController;

	void Start () {
		foreach(MonoBehaviour script in GetComponents(typeof(MonoBehaviour))) {
			if (script.GetType().Name != "PlayerController") {
				script.enabled = false;
			}
        }
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		SetPlayerColor (colorIndex);
	}

	void Update() {
		if (gameController.searchForNewPlayers) {
			if (Device.LeftBumper.WasPressed) {
				SetPlayerColor (false);
			}
			if (Device.RightBumper.WasPressed) {
				SetPlayerColor (true);
			}
		}
	}

    public void SetPlayername(string _name) {
        playername = _name;
        playernameText.text = _name;
    }

	void SetPlayerColor(bool _nextIndex) {
		int totalColors = gameController.playerColors.Length;
		colorIndex = (_nextIndex) ?	colorIndex + 1 : colorIndex - 1;
		if (colorIndex >= totalColors) {
			colorIndex = 0;
		} else if (colorIndex < 0) {
			colorIndex = totalColors - 1;
		}
		SetPlayerColor (colorIndex);
	}

	public void SetPlayerColor(int _colorIndex) {
		Color _color = gameController.playerColors [_colorIndex];
		color = _color;
		model.GetComponent<Renderer> ().material.color = color;
		Device.SetLightColor (color);
	}
}
