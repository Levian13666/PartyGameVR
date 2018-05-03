using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 GamePosition;
    public Vector3 LobbyPosition;
    
	[HideInInspector] public bool isInGame = false;

	public bool isRotatingAroundPlayer = false;
	public Transform rotatingPosition;
	public Transform rotatingPosition1;
	public Transform rotatingPosition2;

    bool isShaking = false;
    float shakeDuration = 0f;
    float shakeAmount = 0.4f;
    Vector3 originalPos;

    void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			//rotatingPosition = rotatingPosition1;
			StartCoroutine (RotateAroundPlayerEnum ());
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			//rotatingPosition = rotatingPosition2;
			StartCoroutine (RotateAroundPlayerEnum ());
		}
        if (isShaking) {
            if (shakeDuration > 0) {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime;
            } else {
                shakeDuration = 0f;
                transform.localPosition = originalPos;
                isShaking = false;
            }
		} else if (isRotatingAroundPlayer) {
			transform.RotateAround(rotatingPosition.position, Vector3.up, 20 * Time.deltaTime);
		} else {
            if (isInGame) {
                transform.position = Vector3.Lerp(transform.position, GamePosition, 0.5f * Time.deltaTime);
				transform.eulerAngles = new Vector3(35,0,0);
            } else {
                transform.position = Vector3.Lerp(transform.position, LobbyPosition, 1f * Time.deltaTime);
            }
        }
    }

    public void ShakeCamera() {
        originalPos = transform.localPosition;
        shakeDuration = 0.5f;
        isShaking = true;
    }

	public void RotateAroundPlayer(Transform _playerPosition) {
		rotatingPosition = _playerPosition;
		StartCoroutine (RotateAroundPlayerEnum ());
	}

	IEnumerator RotateAroundPlayerEnum() {
		isRotatingAroundPlayer = true;
		transform.position = rotatingPosition.position + (rotatingPosition.forward * 3); //+ new Vector3 (-1.33, 2.38f, 0);
		transform.eulerAngles = new Vector3 (20f, 160f, 0);

		yield return new WaitForSeconds (6f);

		transform.position = GamePosition;
		//transform.eulerAngles = new Vector3(35f,0,0);
		isRotatingAroundPlayer = false;
	}

}
