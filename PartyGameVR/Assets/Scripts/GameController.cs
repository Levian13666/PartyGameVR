using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameController : MonoBehaviour {

    public List<MonoBehaviour> gameScripts = new List<MonoBehaviour>();

    public UIController UIController;
    public GameObject playerPrefab;
    public Transform playerPositions;
    public Transform playersWrapper;
    public PlayerController[] players = new PlayerController[4];

    bool searchForNewPlayers = true;
    const int maxPlayers = 4;

	void Start () {
        InputManager.OnDeviceDetached += OnDeviceDetached;
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }
	
	void Update () {
        if (searchForNewPlayers) {
            var inputDevice = InputManager.ActiveDevice;

            if (JoinButtonWasPressedOnDevice(inputDevice)) {
                if (ThereIsNoPlayerUsingDevice(inputDevice)) {
                    CreatePlayer(inputDevice);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return)) {
                StartGame();
            }
        }
    }

    bool JoinButtonWasPressedOnDevice(InputDevice inputDevice) {
        return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
    }

    PlayerController FindPlayerUsingDevice(InputDevice inputDevice) {
        var playerCount = GetPlayerCount();
        for (var i = 0; i < players.Length; i++) {
            var player = players[i];
            if (player != null) {
                if (player.Device == inputDevice) {
                    return player;
                }
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
        if (GetPlayerCount() < maxPlayers) {
            int playerIndex;
            if (inputDevice.Action1.WasPressed) {
                playerIndex = 0; // Kryds
            } else if (inputDevice.Action2.WasPressed) {
                playerIndex = 1; // Rund
            } else if (inputDevice.Action3.WasPressed) {
                playerIndex = 2; // Firkant
            } else if (inputDevice.Action4.WasPressed) {
                playerIndex = 3; // Trekant
            } else {
                return null;
            }
            if (players[playerIndex] != null) {
                return null;
            }

            var playerPosition = playerPositions.GetChild(playerIndex);

            GameObject go = Instantiate(playerPrefab, playerPosition);
            go.transform.parent = playersWrapper;
            PlayerController player = go.GetComponent<PlayerController>();
            player.Device = inputDevice;
            players[playerIndex] = player;
            player.index = playerIndex;
            playerPositions.GetChild(playerIndex).GetComponent<StartPosition>().SetStatusText("Spiller " + (playerIndex + 1));
            player.SetPlayername("Spiller " + (playerIndex + 1));

            return player;
        }

        return null;
    }

    int GetPlayerCount() {
        int count = 0;
        for (int i = 0; i < players.Length; i++) {
            if (players[i] != null) {
                count++;
            }
        }
        return count;
    }

    int GetEmptyPlayerIndex() {
        for(int i = 0; i < players.Length; i++) {
            if (players[i] == null) {
                return i;
            }
        }
        return -1;
    }

    void RemovePlayer(PlayerController player) {
        int playerIndex = GetPlayerIndex(player);
        players[playerIndex] = null;
        player.Device = null;
        Destroy(player.gameObject);
    }

    int GetPlayerIndex(PlayerController player) {
        for (int i = 0; i < players.Length; i++) {
            if (players[i] == player) {
                return i;
            }
        }
        return -1;
    }

    void StartGame() {
        searchForNewPlayers = false;
        ActivatePlayerPositions(false);
        gameScripts[0].enabled = true;
    }

    void ActivatePlayerPositions(bool _bool) {
        foreach(Transform go in playerPositions) {
            go.gameObject.SetActive(_bool);
        }
    }

    public List<int> GetPlayerIndexes() {
        List<int> playerList = new List<int>();
        for(int i = 0; i < players.Length; i++) {
            if (players[i] != null) {
                playerList.Add(i);
            }
        }
        return playerList;
    }

}
