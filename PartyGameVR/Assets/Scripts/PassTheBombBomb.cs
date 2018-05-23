using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class PassTheBombBomb : MonoBehaviour {

    public bool isStill;
    public float bombTime;

    [SerializeField] ParticleSystem particleSparkles;
    [SerializeField] Transform particleExplosion;
    Animator animator;
    bool goingToBlow;
    bool blowing;
    PassTheBomb passTheBombController;

    private Vector3 oldPosition;
    private Quaternion oldRotation;

    private float attachTime;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);


    void Awake() {
        isStill = true;
        goingToBlow = false;
        blowing = false;
        animator = GetComponent<Animator>();
        passTheBombController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PassTheBomb>();
    }

    void HandHoverUpdate(Hand hand) {
        if (hand.GetStandardInteractionButtonDown() || ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))) {
            if (hand.currentAttachedObject != gameObject) {
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform.position;
                oldRotation = transform.rotation;

                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(GetComponent<Interactable>());

                // Attach this object to the hand
                hand.AttachObject(gameObject, attachmentFlags);
            } else {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(GetComponent<Interactable>());

                // Restore position/rotation
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
        }
    }

    void Update() {
        if (!passTheBombController.gameStarted) {
            return;
        }
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
