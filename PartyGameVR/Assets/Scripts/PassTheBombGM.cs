using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTheBombGM : MonoBehaviour {

    PassTheBombUI UI;
    PassTheBomb bombController;

    [SerializeField] KeyCode[] bombKeys = new KeyCode[3];
    [SerializeField] KeyCode[] abilityKeys = new KeyCode[1];

    float bombSeconds = -1;
    int chosenPlayerIndex = -1;
    int abilityChosen = -1;


    void Start () {
        bombController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PassTheBomb>();
        UI = bombController.UI;
	}
	
	void Update () {
        PlayerPressed();

        if (!bombController.isBombInPlay) {
            // SMID BOMBE //
            IsBombPressed();

            if (bombSeconds != -1 && chosenPlayerIndex != -1) {
                GiveBombToPlayer(chosenPlayerIndex, bombSeconds);
            }
        } else {
            IsAbilityPressed();
        }
	}

    void PlayerPressed() {
        int playerIndexPressed = -1;
        for (int i = 0; i < GetComponent<PlayerControllerGM>().playerKeys.Length; i++) {
            if (Input.GetKeyDown(GetComponent<PlayerControllerGM>().playerKeys[i])) {
                playerIndexPressed = i;
            }
        }

        if (playerIndexPressed != -1) {
            if (bombController.GetComponent<GameController>().IsIndexAPlayer(playerIndexPressed)) {
                chosenPlayerIndex = (chosenPlayerIndex != playerIndexPressed) ? playerIndexPressed : -1;
            }
        }
    }

    void IsBombPressed() {
        int keyIndex = -1;
        for (int i = 0; i < bombKeys.Length; i++) {
            if (Input.GetKeyDown(bombKeys[i])) {
                keyIndex = i;
                UI.GMBombChosen();
                break;
            }
        }
        switch(keyIndex) {
            case 0:
				bombSeconds = Random.Range(bombController.bombTypes[0].MinimumBombTime, bombController.bombTypes[0].MaximumBombTime);
                break;
            case 1:
				bombSeconds = Random.Range(bombController.bombTypes[1].MinimumBombTime, bombController.bombTypes[1].MaximumBombTime);
                break;
            case 2:
				bombSeconds = Random.Range(bombController.bombTypes[2].MinimumBombTime, bombController.bombTypes[2].MaximumBombTime);
	            break;
            default:
                break;
        }
    }

    void IsAbilityPressed() {
        for (int i = 0; i < abilityKeys.Length; i++) {
            if (Input.GetKeyDown(abilityKeys[i])) {
                abilityChosen = i;
                UI.GMAbilityChosen(i);
                break;
            }
        }
        if (chosenPlayerIndex == -1) return;

        switch (abilityChosen) {
            case 0:
                // Freeze
                FreezePlayer(chosenPlayerIndex);
                break;
            case 1:
                //
                break;
            case 2:
                //
                break;
            default:
                break;
        }
        chosenPlayerIndex = -1;
        abilityChosen = -1;
    }

    void FreezePlayer(int _index) {
        print("Frys spiller " + _index);
        bombController.GetComponent<GameController>().players[_index].GetComponent<PassTheBombPlayer>().Freeze();
    }

    void GiveBombToPlayer(int _playerIndex, float _bombSeconds) {
        bombSeconds = -1;
        chosenPlayerIndex = -1;

        bombController.AddBomb(_playerIndex, _bombSeconds);
        UI.BombInPlay(true);
    }

    public void Restart() {
        UI.RestartRound();
        bombSeconds = -1;
        chosenPlayerIndex = -1;
        abilityChosen = -1;
    }

}
