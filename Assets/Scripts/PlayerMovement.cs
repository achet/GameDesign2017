using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    bool moving = false;
    public float speed = 6f;
    public float jumpSpeed = 50f;
    public float timeToMove;
    public float distanceToLane;

    Vector3 movement; // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;
    float camRayLength = 100f;

    void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float currentZ = transform.position.z;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space")) { playerRigidbody.AddForce(Vector3.up * jumpSpeed); }

        if (v == 1 && currentZ < 4) {
            GameObject.Find("Main Camera").GetComponent<CameraFlip>().setAbove(timeToMove);
            if (!moving) {  StartCoroutine(MoveObject(transform, this.transform.position, calcTargetPos(distanceToLane), timeToMove)); }
        }
        else if(v == -1 && currentZ > 1) {
            GameObject.Find("Main Camera").GetComponent<CameraFlip>().setRight(timeToMove);
            if (!moving) { StartCoroutine(MoveObject(transform, this.transform.position, calcTargetPos(-distanceToLane), timeToMove)); }
        }

        Move(h, v); // Move the player around the scene.
    }

    Vector3 calcTargetPos(float zValue) {
        return new Vector3(transform.position.x, transform.position.y, (transform.position.z + zValue)); 
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
        moving = true; // MoveObject started
        float i = 0;
        float rate = 1 / time;
        while (i < 1) {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return 0;
        }
        moving = false; // MoveObject ended
    }

    void Move(float h, float v) {
        movement.Set(h, 0f, v); 
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement); // Move the player to it's current position plus the movement.
    }
}