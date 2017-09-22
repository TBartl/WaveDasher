using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	public static TimeManager S;
	public float delay = .03f;

	void Awake() {
		S = this;
	}

	public void StunPause() {
		StartCoroutine(StunPauseCo());
	}

	IEnumerator StunPauseCo() {
		Time.timeScale = 0f;
		for (float endTime = Time.realtimeSinceStartup + delay; Time.realtimeSinceStartup < endTime; ) {
			yield return null;
		}
		Time.timeScale = 1;


	}
}
