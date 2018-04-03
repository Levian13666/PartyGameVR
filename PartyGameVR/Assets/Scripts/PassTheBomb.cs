using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTheBomb : MonoBehaviour {

    public PlayerControllerGM gmPlayer;
    public PlayerController[] players;
    [HideInInspector] public PassTheBombUI UI;

    public int pointsPrSecond = 1;
    public bool gameStarted = false;
    public bool isBombInPlay = false;
    public Transform bombPositions;
    public Vector3 bombStartPosition;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] AudioClip tickingBomb;
    [SerializeField] AudioClip explodingBomb;

    List<GameObject> bombsInPlay = new List<GameObject>();

    AudioSource audioSource;

	[Header("Debug")]
	public bool bombExploding = true;
    public int bombMinTime = 5;
    public int bombMaxTime = 10;


    void Start() {
        gmPlayer = GetComponent<GameController>().gmPlayer;
        players = GetComponent<GameController>().players;
        UI = GetComponent<GameController>().UIController.PassTheBombUI.GetComponent<PassTheBombUI>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartGame());
    }

    void Update() {
        if (!gameStarted) return;

        if (!isBombInPlay) {
            if (!gmPlayer) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    RestartGame();
                    AddBomb();
                }
            }
        }
    }

    IEnumerator StartGame() {
        gameStarted = true;
        for(int i = 0; i < players.Length; i++) {
            PlayerController player = players[i];
            if (player != null) {
                player.GetComponent<PassTheBombPlayer>().enabled = true;
                player.GetComponent<PassTheBombPlayer>().playerUI = UI.playerUIWrapper.GetChild(i).GetComponent<PassTheBombPlayerUI>();
                player.GetComponent<PassTheBombPlayer>().playerUI.ShowUI(true);
            }
        }
        UI.gameObject.SetActive(true);

        if (gmPlayer != null) {
            gmPlayer.GetComponent<PassTheBombGM>().enabled = true;
        }

        GetComponent<GameController>().UIController.SetStatusText("Ny runde starter");

        yield return new WaitForSeconds(3);

        if (!gmPlayer) {
            AddBomb();
        }
    }

    public void AddBomb(int _playerIndex = -1, float _bombTime = -1f) {
        print("Placerer bombe");

        isBombInPlay = true;
        List<int> playerIndexes = GetComponent<GameController>().GetPlayerIndexes();
        int playerIndexToGetBomb = (_playerIndex == -1) ? Random.Range(0, playerIndexes.Count) : _playerIndex;
        PlayerController playerGetBomb = (_playerIndex == -1) ? players[playerIndexes[playerIndexToGetBomb]] : players[playerIndexToGetBomb];
        playerGetBomb.GetComponent<PassTheBombPlayer>().ReceiveBomb();
        GetComponent<GameController>().UIController.SetStatusText(playerGetBomb.playername + " har bomben!");

        GameObject bomb = Instantiate(bombPrefab, bombStartPosition, Quaternion.identity);
        bomb.GetComponent<PassTheBombBomb>().bombTime = (_bombTime == -1) ? Random.Range(bombMinTime, bombMaxTime) : _bombTime;
        bombsInPlay.Add(bomb);
        PlaceBomb(bomb, playerGetBomb.index);

        audioSource.clip = tickingBomb;
        audioSource.loop = true;
        audioSource.Play();
    }

    void PlaceBomb(GameObject _bomb, int _playerIndex) {
        Vector3 position = bombPositions.GetChild(_playerIndex).transform.position;
        _bomb.GetComponent<PassTheBombBomb>().SetNewPosition(position);
    }

    public void BlowBomb() {
        isBombInPlay = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explodingBomb);
        for(int i = 0; i < players.Length; i++) {
            if (players[i] == null) continue;
            if (players[i].GetComponent<PassTheBombPlayer>().hasBomb) {
                players[i].GetComponent<PassTheBombPlayer>().BombExploded();
                break;
            }
        }
        GetComponent<GameController>().cameraController.ShakeCamera();
    }

    public void SendBombToPlayer(int _fromIndex, int _toIndex) {
        if (players[_toIndex] == null || !isBombInPlay) {
            return;
        }
        GameObject bomb = null;
        for(int i = 0; i < bombsInPlay.Count; i++) {
            if (bombsInPlay[i].transform.position == bombPositions.GetChild(_fromIndex).transform.position) {
                bomb = bombsInPlay[i];
                break;
            }
        }

        if (bomb == null) return;  
        if (!bomb.GetComponent<PassTheBombBomb>().isStill)  return;
        if (players[_toIndex].GetComponent<PassTheBombPlayer>().isFreezed) return;
        PlaceBomb(bomb, _toIndex);

        players[_fromIndex].GetComponent<PassTheBombPlayer>().SentBomb();
        players[_toIndex].GetComponent<PassTheBombPlayer>().ReceiveBomb();
    }

    void RestartGame() {
        foreach(PlayerController player in players) {
            if (player == null) continue;
            player.GetComponent<PassTheBombPlayer>().Restart();
        }
        foreach(GameObject bomb in bombsInPlay) {
            Destroy(bomb);
        }
        bombsInPlay.Clear();

        gmPlayer.GetComponent<PassTheBombGM>().Restart();
    }

}
