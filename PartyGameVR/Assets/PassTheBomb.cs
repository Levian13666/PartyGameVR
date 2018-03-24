using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTheBomb : MonoBehaviour {

    public bool gameStarted = false;
    bool isBombInPlay = false;

    private void Update() {
        if (!gameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space)) {

        }
    }

    public void StartGame() {
        gameStarted = true;
        AddBomb();
    }

    void AddBomb() {
        isBombInPlay = true;
        print("Placér bombe");
    }


}
