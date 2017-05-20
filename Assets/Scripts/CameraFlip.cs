using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour {

    bool moving = false;
    [SerializeField] bool cameraAbove = false;

    GameObject target;

    Vector3 aboveRot = new Vector3(60, 0, 0);
    Vector3 sideRot = new Vector3(15, 0, 0);

    void Start () {
        target = GameObject.Find("Player");
    }

    Vector3 getUpdatedPos(string posType) {
        if (posType == "above") { return new Vector3(target.transform.position.x, 6.5f, 2); }
        else { return new Vector3(target.transform.position.x, 6.5f, -10); } // need to make sure that the y value is equal to the yOffset of the camera plus half of the player height    
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float time) {
        moving = true; // MoveObject started
        float i = 0;
        float rate = 1 / time;
        while (i < 1) {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            thisTransform.rotation = Quaternion.Slerp(startRot, endRot, i);
            yield return 0;
        }
        moving = false; // MoveObject ended
    }

    public void setAbove(float delay) {
        if (!moving) { // never start a new MoveObject while it's already running!
            StartCoroutine(MoveObject(transform, getUpdatedPos("side"), getUpdatedPos("above"), Quaternion.Euler(sideRot), Quaternion.Euler(aboveRot), delay));
        }
    }

    public void setRight(float delay) {
        if (!moving) { // never start a new MoveObject while it's already running!
            StartCoroutine(MoveObject(transform, getUpdatedPos("above"), getUpdatedPos("side"), Quaternion.Euler(aboveRot), Quaternion.Euler(sideRot), delay));
        }
    }
}
