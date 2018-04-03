using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 GamePosition;
    public Vector3 LobbyPosition;
    [HideInInspector] public bool isInGame = false;

    bool isShaking = false;
    float shakeDuration = 0f;
    float shakeAmount = 0.4f;
    Vector3 originalPos;

    void Update () {
        if (isShaking) {
            if (shakeDuration > 0) {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime;
            } else {
                shakeDuration = 0f;
                transform.localPosition = originalPos;
                isShaking = false;
            }
        } else {
            if (isInGame) {
                transform.position = Vector3.Lerp(transform.position, GamePosition, 0.5f * Time.deltaTime);
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

}
