using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {
	Transform mainCamera;
    public float camDistance = 10;
    public float speed = 1;
    Quaternion rotation;

    // Use this for initialization
    void Start () {
        rotation = this.transform.rotation;

		mainCamera = Camera.main.transform.parent;
		mainCamera.rotation = rotation;
		UpdateCamera();
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = rotation;
        UpdateCamera();
	}

	void UpdateCamera() {
        Vector3 targetPoint = this.transform.position - transform.forward* camDistance;
		float dist = Vector3.Distance(mainCamera.position, targetPoint);
		mainCamera.position = Vector3.Lerp(mainCamera.position, targetPoint, dist * Time.deltaTime * speed);
	}
}
