using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTheBomb : MonoBehaviour {

    PlayerController[] players;
    PassTheBombUI UI;

    public int pointsPrSecond = 1;
    public bool gameStarted = false;
    public bool isBombInPlay = false;

    [SerializeField] AudioClip tickingBomb;
    [SerializeField] AudioClip explodingBomb;

    AudioSource audioSource;
    float bombTime;

	[Header("Debug")]
	public bool bombExploding = true;


    void Start() {
        players = GetComponent<GameController>().players;
        UI = GetComponent<GameController>().UIController.PassTheBombUI.GetComponent<PassTheBombUI>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartGame());
    }

    void Update() {
        if (!gameStarted) return;

        if (isBombInPlay) {
			if (bombExploding) { // DEBUG
				bombTime -= Time.deltaTime;
			}

            if (bombTime < 0) {
                isBombInPlay = false;
                BlowBomb();
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                RestartGame();
                AddBomb();
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

        GetComponent<GameController>().UIController.SetStatusText("Ny runde starter");

        yield return new WaitForSeconds(3);

        AddBomb();
    }

    void AddBomb() {
        print("Placerer bombe");

        isBombInPlay = true;
        List<int> playerIndexes = GetComponent<GameController>().GetPlayerIndexes();
        int playerIndexToGetBomb = Random.Range(0, playerIndexes.Count);
        PlayerController playerGetBomb = players[playerIndexes[playerIndexToGetBomb]];
        playerGetBomb.GetComponent<PassTheBombPlayer>().hasBomb = true;
        GetComponent<GameController>().UIController.SetStatusText(playerGetBomb.playername + " har bomben!");

        audioSource.clip = tickingBomb;
        audioSource.loop = true;
        audioSource.Play();

        bombTime = Random.Range(5f, 10f);
    }

    void BlowBomb() {
        print("Bomben sprang!");
        audioSource.Stop();
        //audioSource.clip = explodingBomb;
        //audioSource.loop = false;
        audioSource.PlayOneShot(explodingBomb);

    }

    public void SendBombToPlayer(int _fromIndex, int _toIndex) {
        if (players[_toIndex] == null || !isBombInPlay) {
            return;
        }

        print("Sender bombe til " + players[_toIndex].playername);
        players[_fromIndex].GetComponent<PassTheBombPlayer>().SentBomb();
        players[_toIndex].GetComponent<PassTheBombPlayer>().ReceiveBomb();
    }

    void RestartGame() {
        foreach(PlayerController player in players) {
            if (player == null) continue;
            player.GetComponent<PassTheBombPlayer>().Restart();
        }
    }

}
