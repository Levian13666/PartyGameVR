using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

    public InputDevice Device { get; set; }
    public string playername;
    public int index;
	public int colorIndex = 0;
	public int characterIndex = 0;

	public Color color;

    [SerializeField] TextMesh playernameText;
	//[SerializeField] GameObject model;
	public Transform model;

	GameController gameController;

	void Start () {
		foreach(MonoBehaviour script in GetComponents(typeof(MonoBehaviour))) {
			if (script.GetType().Name != "PlayerController") {
				script.enabled = false;
			}
        }
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		SetPlayerColor (true);
		SetPlayerCharacter(true);
    }

    void Update() {
		if (gameController.searchForNewPlayers) {
			if (Device.LeftBumper.WasPressed) {
				SetPlayerCharacter(false);
			}
			if (Device.RightBumper.WasPressed) {
				SetPlayerCharacter(true);
			}
            if (Device.LeftTrigger.WasPressed) {
                SetPlayerColor (false);
            }
            if (Device.RightTrigger.WasPressed) {
                SetPlayerColor(true);
            }
		}
	}

    public void SetPlayername(string _name) {
        playername = _name;
        playernameText.text = _name;
    }

	void SetPlayerColor(bool _nextIndex) {
		int totalColors = gameController.playerColors.Length;
		for(int i = 0; i < totalColors; i++) {
			bool colorAccepted = true;
			colorIndex = (_nextIndex) ? colorIndex + 1 : colorIndex - 1;
			if (colorIndex >= totalColors) {
				colorIndex = 0;
			} else if (colorIndex < 0) {
				colorIndex = totalColors - 1;
			}
			for (int x = 0; x < gameController.players.Length; x++) {
				if (gameController.players[x] == this || gameController.players[x] == null) continue;
				if (gameController.players[x].colorIndex == colorIndex) {
					colorAccepted = false;
					break;
				}
			}
			if (colorAccepted) break;
		}
		SetPlayerColor (colorIndex);
	}

	public void SetPlayerColor(int _colorIndex) {
		Color _color = gameController.playerColors [_colorIndex];
		color = _color;
        foreach (Renderer ren in model.GetComponentsInChildren<Renderer>()) {
            ren.material.color = color;
        }
		Device.SetLightColor (color);
	}

	

	void SetPlayerCharacter(bool _nextIndex) {
		int totalCharacters = gameController.playerCharacters.Length;
		for(int i = 0; i < totalCharacters; i++) {
			bool characterAccepted = true;
			characterIndex = (_nextIndex) ? characterIndex + 1 : characterIndex - 1;
			if (characterIndex >= totalCharacters) {
				characterIndex = 0;
			} else if (characterIndex < 0) {
				characterIndex = totalCharacters - 1;
			}
			for (int x = 0; x < gameController.players.Length; x++) {
				if (gameController.players[x] == this || gameController.players[x] == null) continue;
				if (gameController.players[x].characterIndex == characterIndex) {
					characterAccepted = false;
					break;
				}
			}
			if (characterAccepted) break;
		}
		SetPlayerCharacter (characterIndex);
	}

	public void SetPlayerCharacter(int _characterIndex) {
		foreach(Transform child in model) {
			Destroy (child.gameObject);
		}
		GameObject _character = Instantiate(gameController.playerCharacters [_characterIndex], model);
		playername = _character.GetComponent<ModelController> ().name;

        Color _color = gameController.playerColors [colorIndex];
		color = _color;
        _character.GetComponent<ModelController>().SetupModel(color);

		Device.SetLightColor (color);

	}

}
