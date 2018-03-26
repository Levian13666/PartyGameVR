using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PassTheBombPlayer : MonoBehaviour {

    public bool hasBomb = false;

    [SerializeField] GameObject bomb;
    InputDevice inputDevice;
    PassTheBomb controller;
    int playerIndex;

    void Start() {
        inputDevice = GetComponent<PlayerController>().Device;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<PassTheBomb>();
    }

    void Update() {
        bomb.SetActive(hasBomb);

        if (hasBomb) {
            if (inputDevice.Action1.WasPressed) {
                playerIndex = 0; // Kryds
            } else if (inputDevice.Action2.WasPressed) {
                playerIndex = 1; // Rund
            } else if (inputDevice.Action3.WasPressed) {
                playerIndex = 2; // Firkant
            } else if (inputDevice.Action4.WasPressed) {
                playerIndex = 3; // Trekant
            } else {
                playerIndex = -1;
            }

            if (playerIndex != -1 && GetComponent<PlayerController>().index != playerIndex) {
                controller.SendBombToPlayer(GetComponent<PlayerController>().index, playerIndex);
            }

        }
    }

    public void ReceiveBomb() {
        hasBomb = true;
    }

    public void SentBomb() {
        hasBomb = false;
    }

    public void Restart() {
        hasBomb = false;
    }
}
