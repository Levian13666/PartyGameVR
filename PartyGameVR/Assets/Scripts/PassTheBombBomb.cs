using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTheBombBomb : MonoBehaviour {

    public bool isStill;
    public float bombTime;

    [SerializeField] ParticleSystem particleSparkles;
    [SerializeField] Transform particleExplosion;
    Animator animator;
    bool goingToBlow;
    bool blowing;

    public void TestFunction() {
        print("Testing");
    }

    public void DoneTesting() {
        print("DONE");
    }

    void Awake() {
        isStill = true;
        goingToBlow = false;
        blowing = false;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (bombTime > 0) {
            bombTime -= Time.deltaTime;
            if (!particleSparkles.isPlaying) {
                particleSparkles.Play();
            }
        } else {
            if (!goingToBlow) {
                goingToBlow = true;
                particleSparkles.Stop();
            } else {
                if (isStill && !blowing) {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<PassTheBomb>().BlowBomb();
                    animator.SetTrigger("Blow");
                    blowing = true;
                    PlayExplosion();
                }
            }
        }
    }

    public void SetNewPosition(Vector3 newPosition) {
        if (!isStill || goingToBlow) {
            return;
        }
        StartCoroutine(MoveToPosition(newPosition, 0.5f));
    }

    IEnumerator MoveToPosition(Vector3 position, float timeToMove) {
        isStill = false;
        animator.SetTrigger("SendBomb");
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        isStill = true;
    }

    void PlayExplosion() {
        particleSparkles.Stop();
        foreach(Transform child in particleExplosion) {
            child.GetComponent<ParticleSystem>().Play();
        }
    }

    public void DestroyBomb() {
        Destroy(gameObject);
    }
}
