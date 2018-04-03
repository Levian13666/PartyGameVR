using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GM_Player : MonoBehaviour {

    [SerializeField] int playerIndex;
    UIKey key;

    void OnEnable () {
        PlayerControllerGM gmPlayer = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().gmPlayer;
        if (gmPlayer != null) {
            key = GetComponentInChildren<UIKey>();
            key.SetLetter(gmPlayer.playerKeys[playerIndex].ToString());
        }
    }
	
}