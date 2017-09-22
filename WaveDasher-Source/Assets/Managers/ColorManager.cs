using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
	public static ColorManager S;
	public Material playerEffect;
	public Material enemyEffect;

	void Awake() {
		S = this;
	}

	public void Colorize(GameObject go, Source affiliation) {
		foreach (Renderer m in go.GetComponentsInChildren<Renderer>()) {
			if (affiliation == Source.player)
				m.material = playerEffect;
			else
				m.material = enemyEffect;
		}
	}
}
