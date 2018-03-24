using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    public Transform[] playerPositions;
    public Transform playersWrapper;
    List<PlayerController> players = new List<PlayerController>(maxPlayers);

    bool searchForNewPlayers = true;
    const int maxPlayers = 4;

	// Use this for initialization
	void Start () {
        InputManager.OnDeviceDetached += OnDeviceDetached;
	}
	
	// Update is called once per frame
	void Update () {
        if (searchForNewPlayers) {
            var inputDevice = InputManager.ActiveDevice;

            if (JoinButtonWasPressedOnDevice(inputDevice)) {
                if (ThereIsNoPlayerUsingDevice(inputDevice)) {
                    CreatePlayer(inputDevice);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return)) {
                searchForNewPlayers = false;
                print("Start Game");
            }
        } else {
            if (!GetComponent<PassTheBomb>().gameStarted) {
                GetComponent<PassTheBomb>().StartGame();
            }
        }

    }

    bool JoinButtonWasPressedOnDevice(InputDevice inputDevice) {
        return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
    }

    PlayerController FindPlayerUsingDevice(InputDevice inputDevice) {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++) {
            var player = players[i];
            if (player.Device == inputDevice) {
                return player;
            }
        }

        return null;
    }

    bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice) {
        return FindPlayerUsingDevice(inputDevice) == null;
    }

    void OnDeviceDetached (InputDevice inputDevice) {
        var player = FindPlayerUsingDevice(inputDevice);
        if (player != null) {
            RemovePlayer(player);
        }
    }

    PlayerController CreatePlayer(InputDevice inputDevice) {
        if (players.Count < maxPlayers) {
            // Pop a position off the list. We'll add it back if the player is removed.
            var playerPosition = playerPositions[players.Count];
            //playerPositions.RemoveAt(0);

            GameObject go = Instantiate(playerPrefab, playerPosition);
            go.transform.parent = playersWrapper;
            var player = go.GetComponent<PlayerController>();
            player.Device = inputDevice;
            players.Add(player);

            return player;
        }

        return null;
    }
    void RemovePlayer(PlayerController player) {
        //playerPositions.Insert(0, player.transform.position);
        players.Remove(player);
        player.Device = null;
        Destroy(player.gameObject);
    }

}
