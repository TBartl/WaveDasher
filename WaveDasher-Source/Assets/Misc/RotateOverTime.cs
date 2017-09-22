using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
	Vector3 rotation;
	public Vector3 delta;
	
	// Update is called once per frame
	void Update () {
		rotation += delta * Time.deltaTime;
		this.transform.localRotation = Quaternion.Euler(rotation);
	}
}
