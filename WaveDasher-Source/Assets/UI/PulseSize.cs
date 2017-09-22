using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseSize : MonoBehaviour {
	public float amount;
	public float speed;
    public float waveEffect;
    public float index;

	// Update is called once per frame
	void Update () {
		this.transform.localScale = Vector3.one * (1 + amount * Mathf.Sin(Time.timeSinceLevelLoad * speed - index));
	}
}
