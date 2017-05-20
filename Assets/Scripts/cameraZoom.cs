using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour {

    [SerializeField] float zoomFactor = 1.0f;
    [SerializeField] float zoomSpeed = 5.0f;

    float originalSize;
    Camera thisCamera;

    void Start () {
        thisCamera = GetComponent<Camera>();
        originalSize = thisCamera.orthographicSize;
	}
	
	void FixedUpdate () {
        float targetSize = originalSize * zoomFactor;
        if (targetSize != thisCamera.orthographicSize) { thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed); }
	}

    void setZoom (float zoomFactor) {
        this.zoomFactor = zoomFactor;
    }
}
