using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControllerGM : MonoBehaviour {

    public KeyCode[] playerKeys = new KeyCode[GameController.maxPlayers];

    [SerializeField] GameObject playerVRPrefab;
    [SerializeField] GameObject vrCamera;

    GameObject playerVR;
    GameController gameController;

	void Start () {
        foreach (MonoBehaviour script in GetComponents(typeof(MonoBehaviour))) {
            if (script.GetType().Name != "PlayerControllerGM") {
                script.enabled = false;
            }
        }
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    public void SetupVR() {
        playerVR = Instantiate(playerVRPrefab, transform);
    }

}
