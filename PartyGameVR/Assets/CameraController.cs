using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 GamePosition;
    public Vector3 LobbyPosition;

    public bool isInGame = false;

    private void Start() {
        //LobbyPosition = transform.position;
    }

    void Update () {
		if (isInGame) {
            transform.position = Vector3.Lerp(transform.position, GamePosition, 0.5f * Time.deltaTime);
        } else {
            transform.position = Vector3.Lerp(transform.position, LobbyPosition, 1f * Time.deltaTime);
        }
	}


}
