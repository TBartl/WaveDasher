using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour {
	public float time;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, time);
	}
}
