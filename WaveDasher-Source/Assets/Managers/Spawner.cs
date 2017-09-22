using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public float interval;
	public GameObject prefab;

	void Start() {
		StartCoroutine(SpawnStuff());
	}
	IEnumerator SpawnStuff() {
		while (true) {
			yield return new WaitForSeconds(interval);
			Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
		}
	}
}
