using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform trackingTarget;

    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float followSpeed;

    [SerializeField] bool isXLocked = false;
    [SerializeField] bool isYLocked = false;

    void Start() {

    }

    public void setXOffset(float amount) {
        xOffset = amount;
    }

    public void setYOffset(float amount) {
        yOffset = amount;
    }

    void FixedUpdate() {

        float xTarget = trackingTarget.position.x + xOffset;
        float yTarget = trackingTarget.position.y + yOffset;

        float xNew = transform.position.x;
        float yNew = transform.position.y;

        if (!isXLocked) { xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed); }
        if (!isYLocked) { yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed); }

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }
}