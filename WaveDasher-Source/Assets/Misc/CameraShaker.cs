using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {
	public static CameraShaker S;

	public float shakeTime;
	public float amount;

	Transform cam;

	// Use this for initialization
	void Awake () {
		cam = Camera.main.transform;
		cam.parent = this.transform;
		S = this;
	}
	
	public void Shake() {
		StartCoroutine(ShakeCam());
	}
	IEnumerator ShakeCam() {
		for (float t = 0; t < shakeTime; t += Time.deltaTime) {
			cam.localPosition = Random.insideUnitSphere * amount;
			yield return null;
		}
	}
}
